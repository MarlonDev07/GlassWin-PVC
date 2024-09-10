#!/usr/bin/env pwsh

# Confirmar los cambios
git add .
git commit -m "$args"

# Empujar a ambos repositorios
git push origin main
git push secondary main
