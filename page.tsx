import Link from 'next/link';

// Interface para o resumo do produto, alinhada com o ProductSummaryDto.
interface ProductSummary {
  id: string;
  name: string;
  slug: string;
  price: number;
  imageUrl?: string;
}

async function getProducts(): Promise<ProductSummary[]> {
  const apiBaseUrl = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000';

  try {
    const res = await fetch(`${apiBaseUrl}/api/v1/products`, {
      next: { revalidate: 600 } // Cache de 10 minutos.
    });

    if (!res.ok) {
      throw new Error('Falha ao buscar produtos da API.');
    }

    return res.json();
  } catch (error) {
    console.error(error);
    return []; // Retorna um array vazio em caso de erro.
  }
}

// Componente de Card para exibir um único produto.
function ProductCard({ product }: { product: ProductSummary }) {
  return (
    <Link href={`/produtos/${product.slug}`} className="group block border rounded-lg overflow-hidden shadow-sm hover:shadow-md transition-shadow duration-300">
      <div className="w-full h-48 bg-gray-200 flex items-center justify-center">
        {/* Placeholder para a imagem do produto */}
        <span className="text-gray-500">Imagem</span>
      </div>
      <div className="p-4">
        <h3 className="text-md font-semibold text-gray-800 group-hover:text-blue-600 transition-colors">{product.name}</h3>
        <p className="text-lg font-bold text-gray-900 mt-2">R$ {product.price.toFixed(2)}</p>
      </div>
    </Link>
  );
}

export default async function ProductsPage() {
  const products = await getProducts();

  return (
    <main className="container mx-auto p-6">
      <h1 className="text-3xl font-bold mb-8">Nossos Produtos</h1>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
        {products.map((product) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </div>
    </main>
  );
}