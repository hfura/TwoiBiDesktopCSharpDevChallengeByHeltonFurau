# 2iBi C# Developer Challenge - By Helton Furau

O presente repositorio apresenta uma desktop app para CRIAR, LISTAR, ACTUALIZAR e APAGAR produtos

### Tecnologias usadas:
- Framework .NET 6.0
- C# Windows Forms App
- Microsoft Visual Studio 2022

### Dependencias:
- MySql.Data
- Flurl.Http

### Funcionalidades:
- Criar Produto
- Editar/Actualizar Produto
- Apagar Produto
- Listar Produtos
- Procurar Produto (por identifier, description ou descriptionEN)
- Sincronizar Produtos da API Reqwest com a base de dados local
(As funcionalidades de CRIAR, EDITAR e APAGAR tem efeito tanto na base de dados local como na API do Reqwest instantaneamente)

Video demo em: https://drive.google.com/file/d/1n56dX2UtJSlKRr9VRoSvf7Vp3o2WN90Z/view?usp=sharing

Para testar localmente basta:
* baixar ou clonar o repositorio
* configurar ambiente para C#
* instalar o Visual Studio
* importar o projecto para o Visual Studio
* instalar dependecias
* fazer build do projecto
