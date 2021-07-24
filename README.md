# TruckRegistration

Trata-se de uma solução pequena, apenas como amostra de alguns conceitos.
Pela simplicidade e para focar na lógica, arquitetura da solução e testes unitários, a aplicação não inclui autorização/autenticação.
Também pela simplicidade, não existe código assíncrono e nem programanção paralela no back-end.
Na ocasião de expansão da solução, é ideal separar as camadas de domínio e infraestrutura como bibliotecas dedicadas, e referenciar os projetos conforme a seguir:
- Applicação depende do Domínio, para usar os modelos de dados e interfaces dos repositórios.
- Aplicação também pode depender da Infraestrutura, para registrar as classes que implementam os repositórios.
- Infraestrutura depende do Domínio para usar os modelos de dados e interfaces dos repositórios.
- Domínio não deve ter dependência, já que representa a camada principal da aplicação.

Quanto à aplicação do front-end, o desenvolvimento encontra-se inacabado dada a restrição de tempo.

## Tecnologias

- .NET 5
- Entity Framework Core
- SQL Server
- xUnit, Moq
- ReactJs (não concluído)
- Bootstrap

## Servir a applicação localmente

Para iniciar o projeto, use o comando `dotnet run --project .\TruckRegistration\TruckRegistration.csproj` na raiz do sistema.

Então o projeto será servido nos seguinte endereço: `http://localhost:5000`

Se necessário, permita o modo de navegação não seguro, já que a aplicação não inclui certificado SSL.

## Testes

Para rodar os testes unitários, use o comando `dotnet test` na raiz da aplicação.
Se preferir realizar testes diretamente na API, utilize a coleção Postman providenciada `TruckRegistration.postman_collection.json` para verificar outros comandos.

## Documentação

A aplicação foi construída seguindo alguns padrões de design como adapter e repository. A estruturação de arquivos se assemenlha à arquitetura DDD, apesar da simplicidade.
Também devido à simplicidade, a solução contém apenas dois projetos:
1. TruckRegistration
2. Tests

### Aplicação

* **ClientApp** contém a aplicação front-end ReactJs. Essa aplicação não foi concluída devido ao prazo.
* **Controllers** contém as rotas da API, simular ao modelo MVC.
* **Domain** corresponde à camada principal de uma aplicação, composta idealmente por classes planas e sem dependências.
Ela contém a lógica de negócio principal da aplicação, bem como os contratos de implementação de suas dependências, seguinto o padrão de design adapter.
Num cenário de expansão da aplicação, essa pasta deve ser movida para uma biblioteca dedicada.
* **Exceptions** contém classes para construção de um manipulador de erros customizado, baseado no código de status HTTP.
Devido ao tamanho da aplicação, essa implementação é bem simples.
* **Infrastructure** contém a lógica de acesso de dados, além da configuração do banco de dados, seguindo o padrão de design repository.
Num cenário de expansão da aplicação, essa pasta também pode ser movida para uma biblioteca dedicada.
* **Migrations** contém os arquivos de migração do banco de dados, para habilitar versionamento do banco de dados como código.
* **Models** contém os modelos de dados mapeados como tabela no banco de dados. Deve pertencer à camada de domínio da aplicação.

## Testes unitários

Seguindo a estrutura de pastas apresentada acima, apenas os serviços e repositórios foram incluídos nos testes unitários.
Todas as outras classes são consideradas detalhes de implementação, e dispensam a necessidade de testes.
