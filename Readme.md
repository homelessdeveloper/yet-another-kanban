# Yet Another Kanban

Maybe in the future I will add better description of this project :)


## Table of Contents

- [Installation](#installation)
- [Documentation](#documentation)

## Installation

To get started with the project, follow these steps:

### 1. Clone the repository:

#### Using git over HTTPS:

```sh
git clone https://github.com/SalttySugar/yet-another-kanban.git
```

#### Using github-cli:

```sh
gh repo clone SalttySugar/yet-another-kanban
```

### 2. Install the dependencies:

#### TypeScript/Node dependencies

```sh
pnpm install
```

#### .NET

```sh
dotnet restore
```

### 3. Start the app

#### 3.1 Start docker containers:

In the project root run the follwing  command in your terminal:

```sh
docker compose up -d 
```

#### 3.2 Start the API backend server

Go to the `apps/api` folder and execute following in your terminal:

```sh
dotnet run --project src/Host
```

#### 3.3 Start the Web GUI application

Go to the `apps/api` folder and execute following in your terminal:

if you have @nrwl/nx cli installed globally: 

```sh
nx serve web
```

otherwise:

```sh
npx nx serve web
```

---

## Documentation

### Docusaurus docs

We use [docusaurus](https://docusaurus.io/) to manage public documentation. To run docusaurus locally, follow these
steps:

#### Step 1. Open `docs/docusaurus` folder and run:

```sh
npm i
```

#### Step 2. Run the docs with the following command:

```sh
npm run start
```

By default, this will start a server on `localhost` at port `3000`. Open your browser and visit `http://localhost:3000`
to view the documentation.

