import Link from 'next/link';
import { SearchBar } from './SearchBar';

const NavLink = ({ href, children }: { href: string; children: React.ReactNode }) => (
  <Link href={href} className="text-sm font-medium text-slate-600 hover:text-slate-900 transition-colors">
    {children}
  </Link>
);

export function Header() {
  return (
    <header className="w-full border-b bg-white sticky top-0 z-50">
      <div className="container mx-auto flex h-16 items-center justify-between px-4 gap-8">
        <Link href="/" className="text-xl font-bold text-slate-900">
          NorteMédica
        </Link>
        <nav className="hidden md:flex items-center space-x-6">
          <NavLink href="/">Início</NavLink>
          <NavLink href="/produtos">Produtos</NavLink>
          <NavLink href="/licitacoes">Licitações</NavLink>
          <NavLink href="/quem-somos">Quem Somos</NavLink>
          <NavLink href="/contato">Contato</NavLink>
        </nav>
        <div className="flex-1 flex justify-end">
          <SearchBar />
        </div>
      </div>
    </header>
  );
}