import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { Header } from "@/components/common/Header";
import { Footer } from "@/components/common/Footer";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: {
    default: "Nortemédica Distribuidora",
    template: "%s | Nortemédica",
  },
  description: "Soluções Completas em Saúde para o Setor Público e Privado.",
  openGraph: {
    title: "Nortemédica Distribuidora",
    description: "Soluções Completas em Saúde para o Setor Público e Privado.",
    siteName: "Nortemédica",
  },
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="pt-BR">
      <body className={`${inter.className} bg-slate-50 text-slate-800`}>
        <div className="flex flex-col min-h-screen">
          <Header />
          <main className="flex-grow">{children}</main>
          <Footer />
        </div>
      </body>
    </html>
  );
}