# Índice

1. [Fases](#fases)

2. [O Projeto](#o-projeto)

3. [Tech Challenge Fiap Out-23](#techchallengefiapout23)

4. [Orientações](#orientações)

5. [Executando o projeto](#executando-o-projeto)

   5.1. [Clonar o repositório](#clonar-o-reposítório)

   5.2. [Atualizar o Banco de Dados](#atualizar-o-banco-de-dados)

   5.3. [Executar o projeto](#executar-o-projeto)

## Fases

FASE 5 ######################################################################

FASE 5 ######################################################################

FASE 4 ######################################################################

FASE 4 ######################################################################

FASE 3 ######################################################################

FASE 3 ######################################################################

FASE 2 ######################################################################

FASE 2 ######################################################################

FASE 1 Dotnet + Entity Framework + Azure

## O Projeto

Projeto para uma escola organizar grupos de estudo e trabalho

Requisitos:

- Ter uma api para cadastro de alunos
- Ter uma listagem para ver todos alunos e poder editar ou excluir eles
- Ter uma api para formação de grupos de trabalho
- Ter uma listagem para ver todos grupos e poder editar ou excluir eles

  A listagem completa dos requisitos pode ser visualizada no documento em anexo: [Google Docs](https://docs.google.com/document/d/1aJUAMV5o3qD2xWro_lqz_SSLPPsHLtnYsmpt2oF7-Q4/edit?usp=sharing)

Critério de aceite (requisito mínimo)

- Os alunos não podem ser cadastrados sem o RM (campo obrigatório)
- Os alunos não podem ser cadastrados em mais de um grupo

FASE 1 Dotnet + Entity Framework + Azure

PROPOSTA

Criação de um sistema para os alunos formarem grupos de acordo com matérias disponibilizadas por professores ou coordenadores.

## TechChallengeFiapOut23

Tech Challenge é o projeto que englobará os conhecimentos obtidos em todas as disciplinas da fase. Esta é uma atividade que, a princípio, deve ser desenvolvida em grupo. É importante atentar-se ao prazo de entrega, uma vez que esta trata-se de uma atividade obrigatória: ela vale 60% da nota de todas as disciplinas da fase.

O problema

O Tech Challenge desta fase é o desenvolvimento de um projeto utilizando as boas práticas do DDD (Domain Driven Design) a partir do template do .NET na versão 7 com C#.

Esta aplicação deve ser desenvolvida pensando na modelagem tática do DDD, em todas as camadas e suas responsabilidades.

E, para que esse projeto seja próximo do nosso dia a dia, para elaboração do seu contexto você deve seguir os seguintes passos:

    Escolher um tema para seu time - o tema seria o contexto do seu projeto, como um blog de notícias, um canal de vídeo, uma escola etc.
    Criar um documento apresentando o levantamento de requisitos junto com os critérios de aceite para o desenvolvimento da sua aplicação.
    Desenvolver uma aplicação utilizando uma versão estável do .NET.
    Persistir os dados em um banco de dados.

O que esperamos para o entregável? Você deve enviar o link do Github do projeto do seu grupo incluindo o README preenchido com os passos para o build do seu projeto.

Qualquer dúvida, não deixe de nos chamar no Discord para que alguém da equipe te ajude!

Exemplo de projeto:. https://github.com/Vibra-team/rec

PROPOSTA######################################################################

ORIENTAÇÔES######################################################################

Tech challenge dessa fase @Estudantes 2023/2 🐕
No tech desta fase vocês precisam nos entregar:
Um documento demonstrando o levantamento de requisitos para o desenvolvimento de um sistema (O tema esta a escolha de vocês Ex.: Sistema para um blog, site de games, empresa financeira ...etc)
Um projeto desenvolvido em .NET que tenha uma ou mais entidades que estejam relacionadas ao tema do ponto anterior e que esteja persistindo os dados em um banco de dados

O que esperamos para o entregável?
Link do Github com o projeto completo com Readme preenchido com os passos para executar o projeto e a documentação dele.

Ex.: de um projeto que poderia ser utilizado para o Tech Challenge:
https://github.com/Vibra-team/rec

O documento/critério de aceite será o seu levantamento de requisitos.

Segue exemplo:

Projeto para uma escola

Requisitos:
Ter uma api para cadastro de alunos
Ter uma listagem para ver todos alunos e poder editar ou excluir eles

Critério de aceite (requisito mínimo)
Os alunos não podem ser cadastrados sem o RM (campo obrigatório)

Esse documento pode estar em um word na raiz do projeto ou no próprio readme detalhado com as etapas, exemplo:

Projeto x
O projeto foi desenvolvido pensando em solucionar o problema y

Requisitos:
Como rodar o projeto

Ou em um word essas etapas

## Orientações

ORIENTAÇÔES######################################################################

## Executando o projeto

### Clonar o reposítório

Navegue até o diretório que deseja clonar o projeto em seu computador local utilizando o terminal de sua escolha.
Execute o comando GIT:

```bash
git clone https://github.com/PosTechFiap2023/TechChallengeFiapOut23.git
```

E então navegue até o diretório:

```BASH
cd .\TechChallengeFiapOut23\api
```

### Atualizar o Banco de Dados

Dentro do diretório da API execute o comando dotnet para atualizar o banco de dados:

```bash
dotnet ef database update
```

_ATENÇÂO_ Para o comando acima funcionar é necessário ter o dotnet 7.0 instalado no seu computador e a cli cadastrada no PATH do Windows, ou relativo em outros sistemas operacionais.
Veja como em: [Instalação do dotnet 7.0](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0)

### Executar o projeto

Após o banco de dados estar atualizado basta rodar o seguinte comando no terminal desejado:

```bash
dotnet run
```

ou se desejar execute através do Visual Studio, na opção IIS Express. Caso seja feito pelo Visual Studio a página do swagger abrirá automáticamente, caso seja feito através do cli, basta navegar até o [SWAGGER do projeto](https://localhost:44362/swagger/index.html) no seu navegador favorito.
