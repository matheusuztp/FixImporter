# FixImporter

Uma aplicacao Windows Forms em C# que processa listas de dados e retorna valores distinct com estatisticas.

## Docker (CLI)

Como a interface WinForms nao executa em container Linux, o projeto inclui uma versao CLI com a mesma regra de negocio para uso em Docker.

### Build da imagem

```bash
docker build -t fiximporter:latest .
```

### Executar com variavel de ambiente

```bash
docker run --rm -e INPUT_DATA="PSD-30\n51131131E\nPSD-30" fiximporter:latest
```

### Executar com formato SQL

```bash
docker run --rm -e INPUT_DATA="caixa d'agua\nitem 2\ncaixa d'agua" fiximporter:latest --sql
```

### Executar via stdin

```bash
echo "A\nB\nA" | docker run --rm -i fiximporter:latest
```

### Manter o container ativo para print no docker ps

```bash
docker run -d --name fiximporter-demo -e INPUT_DATA="A\nB\nA" fiximporter:latest --keep-alive
```

```bash
docker ps
```

```bash
docker logs fiximporter-demo
```

```bash
docker rm -f fiximporter-demo
```

A saida mostra a lista distinct e as estatisticas no terminal.
