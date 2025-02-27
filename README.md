FavoriteMoviesAPI
Este é um projeto de API para gerenciar filmes favoritos, desenvolvido em ASP.NET Core. A API permite buscar filmes na OMDb API, gerenciar usuários e salvar filmes favoritos.

📋 Requisitos
.NET 8 SDK

SQL Server (ou SQL Server LocalDB)

Visual Studio Code (ou outra IDE de sua preferência)

Chave de API da OMDb API

🚀 Como Executar o Projeto
1. Clone o Repositório
bash
Copy
git clone https://github.com/seu-usuario/FavoriteMoviesAPI.git
cd FavoriteMoviesAPI
2. Configure o Banco de Dados
Abra o arquivo appsettings.json e atualize a string de conexão do SQL Server:

json
Copy
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=FavoriteMoviesDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
Execute as migrações para criar o banco de dados:

bash
Copy
dotnet ef database update
3. Configure a Chave da OMDb API
Obtenha uma chave de API no site da OMDb API.

Abra o arquivo appsettings.json e adicione a chave:

json
Copy
"OMDb": {
    "ApiKey": "SUA_CHAVE_AQUI",
    "BaseUrl": "http://www.omdbapi.com/"
}
4. Execute o Projeto
No terminal, execute:

bash
Copy
dotnet run
A API estará disponível em:

Copy
http://localhost:5135
Acesse o Swagger para testar os endpoints:

Copy
http://localhost:5135/swagger
🛠️ Endpoints da API
Usuários
POST /api/Users: Cria um novo usuário.

json
Copy
{
  "name": "João Silva",
  "email": "joao@example.com",
  "password": "123456"
}
Filmes
GET /api/Movies: Lista todos os filmes cadastrados.

GET /api/Movies/{id}: Obtém um filme pelo ID.

POST /api/Movies: Adiciona um novo filme.

json
Copy
{
  "title": "Inception",
  "year": 2010,
  "director": "Christopher Nolan",
  "poster": "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SX300.jpg"
}
Filmes Favoritos
GET /api/Users/{userId}/favorites: Lista os filmes favoritos de um usuário.

POST /api/Users/{userId}/favorites: Adiciona um filme favorito para um usuário.

json
Copy
{
  "movieId": 1
}
Busca na OMDb API
GET /api/Movies/search?title={title}: Busca um filme pelo título na OMDb API.

🧪 Testando a API
Você pode testar a API usando o Swagger ou o Postman.

Exemplo de Teste com Swagger
Acesse o Swagger em http://localhost:5135/swagger.

Use os endpoints para criar usuários, adicionar filmes e gerenciar favoritos.

Exemplo de Teste com Postman
Importe a coleção do Postman (se disponível).

Execute as requisições para testar os endpoints.

🐳 Executando com Docker
Se preferir, você pode executar o projeto usando Docker.

Crie uma imagem Docker:

bash
Copy
docker build -t favoritemoviesapi .
Execute o contêiner:

bash
Copy
docker run -p 5135:80 favoritemoviesapi
Acesse a API em:

Copy
http://localhost:5135
📄 Licença
Este projeto está sob a licença MIT. Consulte o arquivo LICENSE para mais detalhes.

🤝 Contribuição
Contribuições são bem-vindas! Siga os passos abaixo:

Faça um fork do projeto.

Crie uma branch para sua feature (git checkout -b feature/nova-feature).

Commit suas alterações (git commit -m 'Adiciona nova feature').

Faça push para a branch (git push origin feature/nova-feature).

Abra um Pull Request.

📧 Contato
Se tiver dúvidas ou sugestões, entre em contato:

Nome: Jhennifer Silva

E-mail: jhennifer.j@gmail.com

GitHub: jhenniferms

Feito com ❤️ por Jhennifer Silva! 🚀
