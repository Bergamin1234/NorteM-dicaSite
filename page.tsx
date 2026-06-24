import { Metadata } from "next";

export const metadata: Metadata = {
  title: "Contato | Nortemédica Distribuidora",
};

export default function ContatoPage() {
  return (
    <div className="container mx-auto p-6 max-w-4xl">
      <h1 className="text-3xl font-bold mb-4">Entre em Contato</h1>
      <div className="prose lg:prose-xl">
        <p>
          Estamos à disposição para atender suas necessidades, seja para uma cotação, participação em licitações ou para conhecer melhor nossas soluções.
        </p>
        <p><strong>Telefone:</strong> (XX) XXXX-XXXX</p>
        <p><strong>Email:</strong> contato@nortemedica.com.br</p>
        <p><strong>Endereço:</strong> Rua Exemplo, 123 - Bairro - Cidade, UF</p>
      </div>
    </div>
  );
}