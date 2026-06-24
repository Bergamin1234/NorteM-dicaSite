import Link from 'next/link';

interface ProductSummary {
  id: string;
  slug: string;
  name:string;
}

interface PaginatedProducts {
  items: ProductSummary[];
  pageNumber: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

interface ProductsPageProps {
  searchParams: { q?: string; page?: string };
}

async function getProducts(searchTerm?: string, page: number = 1): Promise<PaginatedProducts> {
  const apiBaseUrl = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000';
  const params = new URLSearchParams();
  if (searchTerm) {
    params.append('searchTerm', searchTerm);
  }
  params.append('pageNumber', page.toString());
  
  const url = `${apiBaseUrl}/api/v1/products?${params.toString()}`;

  try {
    const res = await fetch(url, { cache: 'no-store' });

    if (!res.ok) {
      console.error('Falha ao buscar produtos:', res.statusText);
      // Retorna um objeto padrão em caso de erro
      return { items: [], pageNumber: 1, totalPages: 0, totalCount: 0, hasPreviousPage: false, hasNextPage: false };
    }

    return res.json();
  } catch (error) {
    console.error("Erro de conexão com a API:", error);
    return { items: [], pageNumber: 1, totalPages: 0, totalCount: 0, hasPreviousPage: false, hasNextPage: false };
  }
}

export default async function ProductsPage({ searchParams }: ProductsPageProps) {
  const searchTerm = searchParams.q;
  const currentPage = Number(searchParams.page) || 1;
  const productsData = await getProducts(searchTerm, currentPage);

  const buildPageLink = (page: number) => {
    const params = new URLSearchParams();
    if (searchTerm) {
      params.append('q', searchTerm);
    }
    if (page > 1) {
      params.append('page', page.toString());
    }
    const queryString = params.toString();
    return `/produtos${queryString ? `?${queryString}` : ''}`;
  };

  return (
    <section className="container mx-auto p-6">
      <h1 className="text-3xl font-bold text-slate-900 mb-4">
        Nossos Produtos
      </h1>

      <form method="GET" className="mb-8 max-w-md">
        <div className="flex">
          <input
            type="text"
            name="q"
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