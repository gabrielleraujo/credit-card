#!/bin/bash

# Listar os arquivos e diretórios em /app (opcional, para depuração)
echo "Conteúdo do diretório /app:"
ls /app
#ls /app/CreditCard.API

# Executar a aplicação
dotnet /app/CreditCard.API.dll
