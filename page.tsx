import { Metadata } from 'next';
import { notFound } from 'next/navigation';

// Interface para os dados do produto, alinhada com o ProductDetailDto do back-end
interface ProductDetail {
  id: string;
  sku: string;
  slug: string;
  name: string;
  description?: string;
  price: number;
  categoryName?: string;
}

// Interface para as props da página
interface ProductPageProps {
  params: { slug: string };
}

// 1. Função de busca de dados centralizada
async function getProductBySlug(slug: string): Promise<ProductDetail | null> {
  const apiBaseUrl = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000';
  try {
    const res = await fetch(`${apiBaseUrl}/api/v1/products/${slug}`, {
      next: { revalidate: 3600 } // Revalida a cada 1 hora
    });

    if (!res.ok) {
      return null;
    }

    return res.json();
  } catch (error) {
    console.error("Failed to fetch product:", error);
    return null;
  }
}

// 2. Geração de metadados usando a função de busca
export async function generateMetadata({ params }: ProductPageProps): Promise<Metadata> {
  const product = await getProductBySlug(params.slug);

  if (!product) {
    return {
      title: 'Produto não encontrado',
      description: 'O produto que você está procurando não existe.'
    };
  }

  return {
    title: `${product.name} | Nortemédica Distribuidora`,
    description: product.description || `Compre ${product.name} na Nortemédica.`,
    alternates: {
      canonical: `https://www.nortemedica.com.br/produtos/${product.slug}`,
    },
  };
}

// 3. Componente da página reutilizando a mesma função de busca
export default async function ProductPage({ params }: ProductPageProps) {
  const product = await getProductBySlug(params.slug);

  if (!product) {
    notFound();
  }

  const jsonLd = {
    '@context': 'https://schema.org',
    '@type': 'MedicalProduct',
    'name': product.name,
    'description': product.description,
    'sku': product.sku,
    'mpn': product.sku,
  };

  return (
    <section className="container mx-auto p-6">
      <script type="application/ld+json" dangerouslySetInnerHTML={{ __html: JSON.stringify(jsonLd) }} />
      <h1 className="text-3xl font-bold text-slate-900">{product.name}</h1>
      <p className="text-muted-foreground">{product.description}</p>
    </section>
  );
}