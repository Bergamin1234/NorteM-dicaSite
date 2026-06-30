'use client'; // Este é um Client Component pois usa o hook useState.

import { useState } from 'react';
import { usePathname } from 'next/navigation';
import Image from 'next/image';
import Link from 'next/link';
import { Menu, X } from 'lucide-react'; // Ícones para o menu mobile.

// Componente auxiliar para os links de navegação
const NavLink = ({ href, children }: { href: string; children: React.ReactNode }) => {
  const pathname = usePathname();
  const isActive = pathname === href;

  return (
    <Link
      href={href}
      className={`font-medium transition-colors ${
        isActive
          ? 'text-blue-600'
          : 'text-gray-500 hover:text-gray-900'
      }`}
    >
      {children}
    </Link>
  );
};

export const Header = () => { 
  // Estado para controlar se o menu mobile está aberto ou fechado.
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  // Links de navegação para reutilização.
  const navLinks = [
    { href: '/', label: 'Início' },
    { href: '/produtos', label: 'Produtos' },
    { href: '/licitacoes', label: 'Licitações' },
    { href: '/sobre', label: 'Sobre Nós' },
    { href: '/contato', label: 'Contato' },
  ];

  return (
    <header className="bg-white shadow-md sticky top-0 z-50">
      <div className="container mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo */}
          <div className="flex-shrink-0">
            <Link href="/" aria-label="Página inicial da Nortemédica">
              <Image
                src="/logo.png" // Caminho para a imagem na pasta /public
                alt="Logo da Nortemédica"
                width={160} // Defina a largura real da sua imagem
                height={40} // Defina a altura real da sua imagem
                priority // Ajuda a carregar o logo mais rápido (LCP)
              />
            </Link>
          </div>

          {/* Navegação para Desktop (escondida em telas pequenas) */}
          <nav className="hidden md:flex md:space-x-8">
            {navLinks.map((link) => (
              <NavLink key={link.href} href={link.href}>
                {link.label}
              </NavLink>
            ))}
          </nav>

          {/* Botão do Menu Mobile (visível apenas em telas pequenas) */}
          <div className="md:hidden flex items-center">
            <button onClick={toggleMenu} className="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-gray-500 hover:bg-gray-100">
              <span className="sr-only">Abrir menu principal</span>
              {isMenuOpen ? <X size={24} /> : <Menu size={24} />}
            </button>
          </div>
        </div>
      </div>

      {/* Menu Mobile (aparece/desaparece com base no estado) */}
      {/* 
        Container da animação:
        - `md:hidden`: Garante que só apareça em telas mobile.
        - `overflow-hidden`: Esconde o conteúdo que excede a altura máxima.
        - `transition-all duration-300 ease-in-out`: Define a animação para todas as propriedades que mudarem.
        - `max-h-0` ou `max-h-96`: Controla a altura, criando o efeito de slide.
      */}
      <div className={`md:hidden overflow-hidden transition-all duration-300 ease-in-out ${isMenuOpen ? 'max-h-96' : 'max-h-0'}`}>
        <div className="px-2 pt-2 pb-3 space-y-1 sm:px-3">
          {navLinks.map((link) => (
            <Link
              key={link.href}
              href={link.href}
                onClick={toggleMenu} // Fecha o menu ao clicar em um link
              className="block px-3 py-2 rounded-md text-base font-medium text-gray-700 hover:text-gray-900 hover:bg-gray-50"
            >
              {link.label}
            </Link>
          ))}
        </div>
      </div>
    </header>
  );
};