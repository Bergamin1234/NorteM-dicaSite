import { ContactForm } from '@/components/forms/ContactForm';
import type { Metadata } from 'next';

export const metadata: Metadata = {
  title: 'Contato',
  description: 'Entre em contato com a Nortemédica. Estamos prontos para atender suas necessidades em soluções de saúde.',
};

export default function ContactPage() {
  return (
    <div className="bg-white py-16 sm:py-24">
      <div className="container mx-auto px-4 sm:px-6 lg:px-8">
        <div className="max-w-2xl mx-auto text-center">
          <h1 className="text-3xl font-bold tracking-tight text-gray-900 sm:text-4xl">
            Entre em Contato
          </h1>
          <p className="mt-2 text-lg leading-8 text-gray-600">
            Tem alguma dúvida ou precisa de um orçamento? Preencha o formulário abaixo e nossa equipe responderá o mais breve possível.
          </p>
        </div>

        <div className="mt-16 max-w-xl mx-auto">
          <ContactForm />
        </div>
      </div>
    </div>
  );
}