import { NextRequest, NextResponse } from 'next/server';
import * as z from 'zod';
import nodemailer from 'nodemailer';

// Replicamos o schema de validação no backend por segurança.
const contactFormSchema = z.object({
  name: z.string().min(3),
  email: z.string().email(),
  subject: z.string().min(5),
  message: z.string().min(10),
  recaptchaToken: z.string().min(1, { message: 'reCAPTCHA inválido.' }),
});

export async function POST(req: NextRequest) {
  try {
    const body = await req.json();

    // Valida os dados recebidos com o schema do Zod
    const validation = contactFormSchema.safeParse(body);

    if (!validation.success) {
      // Se a validação falhar, retorna um erro 400 com os detalhes
      return NextResponse.json(validation.error.errors, { status: 400 });
    }

    const { name, email, subject, message, recaptchaToken } = validation.data;

    // 0. Verificar o token do reCAPTCHA com o Google
    const recaptchaSecret = process.env.RECAPTCHA_SECRET_KEY;
    const recaptchaResponse = await fetch('https://www.google.com/recaptcha/api/siteverify', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: `secret=${recaptchaSecret}&response=${recaptchaToken}`,
    });

    const recaptchaData = await recaptchaResponse.json();

    if (!recaptchaData.success) {
      return NextResponse.json({ message: 'Falha na verificação do reCAPTCHA.' }, { status: 400 });
    }

    // Se a verificação for bem-sucedida, continue com o envio do email...

    // 1. Configurar o "transporter" do Nodemailer com as credenciais do .env
    const transporter = nodemailer.createTransport({
      host: process.env.SMTP_HOST,
      port: Number(process.env.SMTP_PORT),
      secure: process.env.SMTP_PORT === '465', // true para a porta 465, false para as outras
      auth: {
        user: process.env.SMTP_USER,
        pass: process.env.SMTP_PASS,
      },
    });

    // 2. Definir as opções do email
    const mailOptions = {
      from: `"${name}" <${email}>`, // O email do remetente
      to: process.env.SMTP_TO_EMAIL, // O email que receberá a mensagem
      replyTo: email,
      subject: `Novo Contato do Site: ${subject}`,
      html: `
        <h1>Nova mensagem de contato</h1>
        <p><strong>Nome:</strong> ${name}</p>
        <p><strong>Email:</strong> ${email}</p>
        <p><strong>Assunto:</strong> ${subject}</p>
        <hr />
        <p><strong>Mensagem:</strong></p>
        <p>${message.replace(/\n/g, '<br>')}</p>
      `,
    };

    // 3. Enviar o email
    await transporter.sendMail(mailOptions);

    return NextResponse.json({ message: 'Mensagem enviada com sucesso!' }, { status: 200 });
  } catch (error) {
    console.error(error);
    if (error instanceof z.ZodError) {
      return NextResponse.json({ message: 'Dados inválidos.', errors: error.errors }, { status: 400 });
    }
    return NextResponse.json({ message: 'Erro interno do servidor.' }, { status: 500 });
  }
}