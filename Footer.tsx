import Link from 'next/link';
import { Facebook, Instagram, Linkedin } from 'lucide-react';

export const Footer = () => {
  return (
    <footer className="bg-slate-100 border-t border-slate-200">
      <div className="container mx-auto px-4 sm:px-6 lg:px-8">
        {/* Seção principal do rodapé com colunas */}
        <div className="py-12 grid grid-cols-1 md:grid-cols-3 gap-8 text-center md:text-left">
          {/* Coluna 1: Logo e breve descrição */}
          <div className="space-y-4">
            <Link href="/" className="text-xl font-bold text-blue-600">
              Nortemédica
            </Link>
            <p className="text-sm text-slate-600">
              Soluções Completas em Saúde para o Setor Público e Privado.
            </p>
          </div>

          {/* Coluna 2: Links Rápidos */}
          <div>
            <h4 className="font-semibold text-slate-800 tracking-wider uppercase">Links</h4>
            <ul className="mt-4 space-y-2">
              <li>
                <Link href="/produtos" className="text-sm text-slate-600 hover:text-slate-900 transition-colors">
                  Produtos
                </Link>
              </li>
              <li>
                <Link href="/licitacoes" className="text-sm text-slate-600 hover:text-slate-900 transition-colors">
                  Licitações
                </Link>
              </li>
              <li>
                <Link href="/sobre" className="text-sm text-slate-600 hover:text-slate-900 transition-colors">
                  Sobre Nós
                </Link>
              </li>
            </ul>
          </div>

          {/* Coluna 3: Redes Sociais */}
          <div>
            <h4 className="font-semibold text-slate-800 tracking-wider uppercase">Siga-nos</h4>
            <div className="mt-4 flex justify-center md:justify-start space-x-4">
              <a href="#" aria-label="Facebook" className="text-slate-500 hover:text-blue-600 transition-colors">
                <Facebook size={24} />
              </a>
              <a href="#" aria-label="Instagram" className="text-slate-500 hover:text-pink-600 transition-colors">
                <Instagram size={24} />
              </a>
              <a href="#" aria-label="LinkedIn" className="text-slate-500 hover:text-sky-700 transition-colors">
                <Linkedin size={24} />
              </a>
            </div>
          </div>
        </div>

        {/* Barra inferior com direitos autorais */}
        <div className="border-t border-slate-200 py-6 text-center text-sm text-slate-500">
          <p>&copy; {new Date().getFullYear()} Nortemédica Distribuidora. Todos os direitos reservados.</p>
        </div>
      </div>
    </footer>
  );
};