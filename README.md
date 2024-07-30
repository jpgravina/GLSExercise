# GLSExercise
## Descrizione
Questo progetto è un esercizio richiesto da GLS per il colloquio del 31/07/2024
L'applicazione recupera dinamicamente i dati storici del prezzo del petrolio da un file JSON durante l'avvio.

## Requisiti
 .NET 6.0 o successivi
  Docker (opzionale per la containerizzazione)

## Come eseguire il progetto
1. Clonare il repository
 ```bash
    git clone https://github.com/jpgravina/GLSExercise.git
    cd OilPriceTrend
    ```

 2. Ripristina i pacchetti e esegui il progetto:
```bash
    dotnet restore
    dotnet run
    ```

3. L'API sarà disponibile all'indirizzo `https://localhost:5001/api/OilPriceTrend`.

## Esecuzione con Docker

1. Costruisci l'immagine Docker:
    ```bash
    docker build -t oilpricetrend .
    ```

2. Esegui il container:
    ```bash
    docker run -d -p 8080:80 oilpricetrend
    ```

3. L'API sarà disponibile all'indirizzo `http://localhost:8080/api/OilPriceTrend`.

## Esecuzione dei test

1. Vai nella directory dei test e esegui i test:
    ```bash
    cd OilPriceTrendTest
    dotnet test
    ```