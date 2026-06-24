'use client';

import { useState, useEffect } from 'react';
import Link from 'next/link';
import { Search } from 'lucide-react';

interface SearchResult {
  slug: string;
  name: string;
}

export function SearchBar() {
  const [searchTerm, setSearchTerm] = useState('');
  const [results, setResults] = useState<SearchResult[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    // Não busca se o termo for muito curto
    if (searchTerm.length < 3) {
      setResults([]);
      return;
    }

    const delayDebounceFn = setTimeout(() => {
      const fetchResults = async () => {
        setIsLoading(true);
        const apiBaseUrl = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000';
        const response = await fetch(`${apiBaseUrl}/api/v1/products?searchTerm=${searchTerm}`);
        const data = await response.json();
        setResults(data);
        setIsLoading(false);
      };

      fetchResults();
    }, 500); // Espera 500ms após o usuário parar de digitar

    return () => clearTimeout(delayDebounceFn);
  }, [searchTerm]);

  return (
    <div className="relative w-full max-w-xs">
      <input
        type="text"
        placeholder="Buscar produtos..."
        className="w-full pl-10 pr-4 py-2 border rounded-md text-sm"
        onChange={(e) => setSearchTerm(e.target.value)}
        value={searchTerm}
      />
      <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-slate-400" />

      {searchTerm.length > 2 && (
        <div className="absolute top-full mt-2 w-full bg-white border rounded-md shadow-lg z-10">
          {isLoading && <div className="p-2 text-sm text-slate-500">Buscando...</div>}
          {!isLoading && results.length === 0 && <div className="p-2 text-sm text-slate-500">Nenhum resultado encontrado.</div>}
          {results.map((result) => (
            <Link key={result.slug} href={`/produtos/${result.slug}`} className="block p-2 hover:bg-slate-100 text-sm" onClick={() => setSearchTerm('')}>
              {result.name}
            </Link>
          ))}
        </div>
      )}
    </div>
  );
}