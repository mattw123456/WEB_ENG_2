# WEB_ENG_2


# Docker Image
- ghcr.io/mattw123456/biletado-api:latest


### Variablen

Das Compose File kann mit einer Vielzahl an Variablen gefüllt werden, um die internen Einstellungen der API zu verändern.

```yaml
- POSTGRES_ASSETS_PORT
- POSTGRES_ASSETS_HOST
- POSTGRES_ASSETS_DBNAME
- POSTGRES_ASSETS_PASSWORD
- POSTGRES_ASSETS_USER
- KEYCLOAK_REALM
- KEYCLOAK_HOST
- KEYCLOAK_PORT
- KEYCLOAK_AUDIENCE
```

## Authentifizierung

Die Authentifizierung läuft über einen KeyCloak Server. Hierfür müssen folgenden Umgebungsvariablen im Compose File gesetzt werden:

```bash
- KEYCLOAK_REALM
- KEYCLOAK_HOST
- KEYCLOAK_PORT
- KEYCLOAK_AUDIENCE
```


## Biletado mit eigener API als Cluster betreiben

Die angepasste _kustomization.yaml_ liegt im Root Ordner: **kustomization.yaml**

```bash
apiVersion: kustomize.config.k8s.io/v1beta1
kind: Kustomization

resources:
  - https://gitlab.com/biletado/kustomize.git//overlays/kind

patches:
  - patch: |-
      - op: replace
        path: /spec/template/spec/containers/0/image
        value: ghcr.io/mattw123456/biletado-api:latest
      - op: replace
        path: /spec/template/spec/containers/0/ports/0/containerPort
        value: 8080
    target:
      group: apps
      version: v1
      kind: Deployment
      name: assets
```

1. Schritt: Hochfahren des originalen Biletado Clusters

```bash
kubectl create namespace biletado
kubectl config set-context --current --namespace biletado
kubectl apply -k https://gitlab.com/biletado/kustomize.git//overlays/kind?ref=main --prune -l app.kubernetes.io/part-of=biletado -n biletado
kubectl rollout status deployment -n biletado -l app.kubernetes.io/part-of=biletado --timeout=600s
kubectl wait pods -n biletado -l app.kubernetes.io/part-of=biletado --for condition=Ready --timeout=120s
```

2. Schritt: API (assets) "austauschen"

Folgenden Befehl ausführen im Ordner wo die kustomization.yaml liegt (**dhbw.WebEngineering.V2.Kustomize/**)

```bash
# execute this in the folder with kustomization.yaml
kubectl apply -k . --prune -l app.kubernetes.io/part-of=biletado -n biletado
kubectl wait pods -n biletado -l app.kubernetes.io/part-of=biletado --for condition=Ready --timeout=120s
```