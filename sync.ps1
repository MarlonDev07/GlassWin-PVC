#!/usr/bin/env pwsh

# Confirmar los cambios
git add .
git commit -m "$args"

# Empujar a la rama master en el repositorio principal (origin)
git push origin master

# Empujar a la rama main en el repositorio secundario (secondary)
git push secondary main
