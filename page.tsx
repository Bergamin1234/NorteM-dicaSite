import Link from 'next/link';

// Interface para o resumo do produto, alinhada com o ProductSummaryDto do C#
interface ProductSummary {
  id: string;
  slug: string;
  name:string;
}

// Props da página, incluindo os parâmetros de busca da URL (ex: ?q=luva)
interface ProductsPageProps {
  searchParams: { q?: string };
}

// Função para buscar os produtos na nossa API C#
async function getProducts(searchTerm?: string): Promise<ProductSummary[]> {
  const apiBaseUrl = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000';
  // Constrói a URL, adicionando o `searchTerm` se ele existir
  const search = searchTerm ? `?searchTerm=${encodeURIComponent(searchTerm)}` : '';
  const url = `${apiBaseUrl}/api/v1/products${search}`;

  try {
    // Usamos 'no-store' para garantir que a busca seja sempre dinâmica e não use cache
    const res = await fetch(url, { cache: 'no-store' });

    if (!res.ok) {
      console.error('Falha ao buscar produtos:', res.statusText);
      return []; // Retorna uma lista vazia em caso de erro
    }

    return res.json();
  } catch (error) {
    console.error("Erro de conexão com a API:", error);
    return []; // Retorna uma lista vazia se a API estiver offline
  }
}

// O componente da página
export default async function ProductsPage({ searchParams }: ProductsPageProps) {
  // Pega o termo de busca da URL
  const searchTerm = searchParams.q;
  // Chama a função para buscar os produtos
  const products = await getProducts(searchTerm);

  return (
    <section className="container mx-auto p-6">
      <h1 className="text-3xl font-bold text-slate-900 mb-4">
        Nossos Produtos
      </h1>

      {/* Formulário de Busca */}
      <form method="GET" className="mb-8 max-w-md">
        <div className="flex">
          <input
            type="text"
            name="q" // O nome 'q' corresponde ao `searchParams.q`
            defaultValue={searchTerm}
            placeholder="Buscar por nome do produto..."
            className="flex-grow p-2 border border-gray-300 rounded-l-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <button
            type="submit"
            className="p-2 px-4 bg-blue-600 text-white rounded-r-md hover:bg-blue-700"
          >
            Buscar
          </button>
        </div>
      </form>

      {/* Lista de Produtos */}
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
        {products.length > 0 ? (
          products.map((product) => (
            <Link href={`/produtos/${product.slug}`} key={product.id} className="border rounded-lg p-4 hover:shadow-lg transition-shadow">
              <h2 className="text-lg font-semibold text-slate-800">{product.name}</h2>
            </Link>
          ))
        ) : (
          <p>Nenhum produto encontrado.</p>
        )}
      </div>
    </section>
  );
}