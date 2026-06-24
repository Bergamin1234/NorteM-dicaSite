import Link from 'next/link';
import { Search } from 'lucide-react'; // Ícone para o campo de busca

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
          <div className="relative w-full max-w-xs">
            <input
              type="text"
              placeholder="Buscar produtos..."
              className="w-full pl-10 pr-4 py-2 border rounded-md text-sm"
            />
            <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-slate-400" />
          </div>
        </div>
      </div>
    </header>
  );
}