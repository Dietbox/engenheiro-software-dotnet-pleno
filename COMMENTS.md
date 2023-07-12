Possíveis melhorias:

- Hoje a aplicação, caso a base não tenha sido criada, cria um usuário admin e roles. Seria melhor a criação deste usuário não ser feita desta maneira, por questão de segurança;
- Seria melhor utilizar Redis para guardar no cache informações de tokens (logout) e produtos. Não foi utilizado por limitações de hardware na minha máquina;
- Adicionar testes
