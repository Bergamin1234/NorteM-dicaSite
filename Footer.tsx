export function Footer() {
  return (
    <footer className="w-full border-t bg-slate-50">
      <div className="container mx-auto py-6 px-4">
        <p className="text-center text-sm text-slate-500">
          © {new Date().getFullYear()} Nortemédica Distribuidora. Todos os direitos reservados.
        </p>
      </div>
    </footer>
  );
}