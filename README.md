# FixImporter

Uma aplicação Windows Forms em C# que processa listas de dados de planilhas Excel e retorna valores distinct com estatísticas.

## 🎯 Funcionalidades

- **Lista Distinct**: Remove duplicatas de um conjunto de dados
- **Estatísticas**: Mostra total de registros, valores únicos e quantidade de duplicidades
- **Formato SQL**: Opção para formatar a saída pronta para usar em queries SQL
- **Escape de Aspas**: Trata automaticamente aspas simples nos dados (`'` → `''`)
- **Clipboard**: Copia automaticamente o resultado para o clipboard (Ctrl+V)

## 📋 Suporte de Entrada

- Dados colados de colunas Excel
- Quebras de linha: `\r\n`, `\r`, `\n`
- Literal `\n` (convertido automaticamente)
- Espaços em branco (removidos automaticamente)

## 💻 Como Usar

1. **Colar dados**: Cole uma coluna de dados do Excel no campo de texto
2. **Configurar opções**:
   - Marque "Pronto para o SQL" se quiser formato SQL
3. **Processar**: Clique em "Iniciar"
4. **Resultado**: 
   - Lista distinct é copiada para o clipboard
   - Estatísticas aparecem no rodapé
   - Cole com Ctrl+V em qualquer lugar

## 📊 Exemplos

### Entrada:
```
PSD-30
51131131E
PSD-4
PSD-30
IR-30
51131131E
```

### Saída (Formato Normal):
```
PSD-30
51131131E
PSD-4
IR-30
```

### Saída (Formato SQL):
```
'PSD-30',
'51131131E',
'PSD-4',
'IR-30'
```

## 🔧 Requisitos

- .NET 8.0 ou superior
- Windows Forms

## 🛠️ Desenvolvimento

```bash
# Clonar o repositório
git clone https://github.com/matheusuztp/FixImporter.git

# Abrir em Visual Studio
# Compilar: Ctrl+Shift+B
# Executar: F5
```

## 📝 Licença

Este projeto é de uso livre.

## ✨ Recursos Especiais

- ✅ Trata dados com aspas: `caixa d'agua` → `'caixa d''agua'`
- ✅ Remove espaços e linhas vazias
- ✅ Suporta múltiplos formatos de quebra de linha
- ✅ Interface simples e intuitiva
- ✅ Mensagens de erro informativas
