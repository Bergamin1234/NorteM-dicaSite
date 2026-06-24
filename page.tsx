import { Metadata } from "next";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

export const metadata: Metadata = {
  title: "Cadastro | Nortemédica Distribuidora",
};

export default function CadastroPage() {
  return (
    <div className="flex items-center justify-center py-12 px-4">
      <div className="w-full max-w-md space-y-8">
        <div>
          <h2 className="mt-6 text-center text-3xl font-bold tracking-tight text-gray-900">
            Crie sua conta corporativa
          </h2>
        </div>
        <form className="mt-8 space-y-6" action="#" method="POST">
          <div className="space-y-4">
            <div>
              <Label htmlFor="cnpj">CNPJ</Label>
              <Input id="cnpj" name="cnpj" type="text" required />
            </div>
            <div>
              <Label htmlFor="razao-social">Razão Social</Label>
              <Input id="razao-social" name="razao-social" type="text" required />
            </div>
            <div>
              <Label htmlFor="email-address">Email de Contato</Label>
              <Input id="email-address" name="email" type="email" required />
            </div>
            <div>
              <Label htmlFor="password">Crie uma Senha</Label>
              <Input id="password" name="password" type="password" required />
            </div>
          </div>
          <div>
            <Button type="submit" className="w-full">Criar Conta</Button>
          </div>
        </form>
      </div>
    </div>
  );
}