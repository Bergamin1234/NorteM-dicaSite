'use client';

import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import * as z from 'zod';
import { useState, useRef } from 'react';
import ReCAPTCHA from 'react-google-recaptcha';

// 1. Definir o schema de validação com Zod
const contactFormSchema = z.object({
  name: z.string().min(3, 'O nome deve ter pelo menos 3 caracteres.'),
  email: z.string().email('Por favor, insira um email válido.'),
  subject: z.string().min(5, 'O assunto deve ter pelo menos 5 caracteres.'),
  message: z.string().min(10, 'A mensagem deve ter pelo menos 10 caracteres.'),
});

// Extrair o tipo do schema para usar no formulário
type ContactFormValues = z.infer<typeof contactFormSchema>;

export const ContactForm = () => {
  const [formStatus, setFormStatus] = useState<{
    submitted: boolean;
    success: boolean;
    message: string;
  } | null>(null);

  const [recaptchaToken, setRecaptchaToken] = useState<string | null>(null);

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
  } = useForm<ContactFormValues>({
    resolver: zodResolver(contactFormSchema),
  });

  // 2. Função para lidar com o envio do formulário
  const onSubmit = async (data: ContactFormValues) => {
    setFormStatus(null);
    try {
      const response = await fetch('/api/contato', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          ...data,
          recaptchaToken, // Inclui o token do reCAPTCHA no envio
        }),
      });

      if (!response.ok) {
        throw new Error('Ocorreu um erro ao enviar a mensagem.');
      }

      setFormStatus({
        submitted: true,
        success: true,
        message: 'Mensagem enviada com sucesso! Entraremos em contato em breve.',
      });
      reset(); // Limpa o formulário
      setRecaptchaToken(null); // Reseta o estado do reCAPTCHA
    } catch (error) {
      setFormStatus({
        submitted: true,
        success: false,
        message: 'Falha ao enviar a mensagem. Por favor, tente novamente mais tarde.',
      });
    }
  };

  // Função auxiliar para renderizar campos
  const renderInput = (id: keyof ContactFormValues, label: string, type = 'text') => (
    <div>
      <label htmlFor={id} className="block text-sm font-medium text-gray-700">{label}</label>
      <input
        type={type}
        id={id}
        {...register(id)}
        className="mt-1 block w-full px-3 py-2 bg-white border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
      />
      {errors[id] && <p className="mt-1 text-sm text-red-600">{errors[id]?.message}</p>}
    </div>
  );

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      {renderInput('name', 'Nome Completo')}
      {renderInput('email', 'Email', 'email')}
      {renderInput('subject', 'Assunto')}
      <div>
        <label htmlFor="message" className="block text-sm font-medium text-gray-700">Mensagem</label>
        <textarea
          id="message"
          rows={4}
          {...register('message')}
          className="mt-1 block w-full px-3 py-2 bg-white border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
        />
        {errors.message && <p className="mt-1 text-sm text-red-600">{errors.message?.message}</p>}
      </div>

      <div className="flex justify-center">
        <ReCAPTCHA
          sitekey={process.env.NEXT_PUBLIC_RECAPTCHA_SITE_KEY!}
          onChange={(token) => setRecaptchaToken(token)}
          onExpired={() => setRecaptchaToken(null)}
        />
      </div>

      <button type="submit" disabled={isSubmitting || !recaptchaToken} className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:bg-gray-400 disabled:cursor-not-allowed">
        {isSubmitting ? 'Enviando...' : 'Enviar Mensagem'}
      </button>

      {formStatus?.submitted && (
        <p className={`text-sm text-center ${formStatus.success ? 'text-green-600' : 'text-red-600'}`}>
          {formStatus.message}
        </p>
      )}
    </form>
  );
};