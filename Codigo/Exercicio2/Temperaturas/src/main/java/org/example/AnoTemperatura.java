package org.example;

public class AnoTemperatura {
    private int ano;
    private int[] temperaturas;
    private int[] periodoMaisIntenso;

    private int inicio;
    private int fim;
    private int maiorAcumulo;
    private long tempoDeExecucao;

    public AnoTemperatura(int[] temperaturas) {
        // 1ª Interpretação: Soma de Temperaturas
        this.temperaturas = temperaturas;

        // 2ª Interpretação: Soma das Diferenças
        // this.temperaturas = converterParaDiferencasEntreDias(temperaturas);

        long tempo = System.nanoTime();
        encontrarMaiorSubArray(this.temperaturas, 0, this.temperaturas.length - 1);
        tempoDeExecucao = System.nanoTime() - tempo;
    }

    private static int[] converterParaDiferencasEntreDias(int[] temperaturas) {
        int[] temperaturasDiferencas = new int[temperaturas.length - 1];
        for (int i = 0; i < temperaturas.length - 1; i++) {
            temperaturasDiferencas[i] = temperaturas[i + 1] - temperaturas[i];
        }
        return temperaturasDiferencas;
    }

    //#region Getters
    public int getAno() {
        return ano;
    }

    public void setAno(int ano) {
        this.ano = ano;
    }

    public int[] getPeriodoMaisIntenso() {
        return periodoMaisIntenso;
    }

    public int getInicio() {
        return inicio;
    }

    public int getFim() {
        return fim;
    }

    public int getMaiorAcumulo() {
        return maiorAcumulo;
    }
    //#endregion

    //#region Métodos Divisão e Conquista
    private static int[] encontrarMaiorSubArrayCruzado(int[] array, int menor, int meio, int maior) {
        int somaEsquerda = Integer.MIN_VALUE;
        int soma = 0;
        int maiorEsquerda = 0;

        for (int i = meio; i >= menor; i--) {
            soma += array[i];
            if (soma > somaEsquerda) {
                somaEsquerda = soma;
                maiorEsquerda = i;
            }
        }
        int somaDireita = Integer.MIN_VALUE;
        soma = 0;
        int maiorDireita = 0;
        for (int i = meio + 1; i <= maior; i++) {
            soma += array[i];
            if (soma > somaDireita) {
                somaDireita = soma;
                maiorDireita = i;
            }
        }
        return new int[]{maiorEsquerda, maiorDireita, somaEsquerda + somaDireita};
    }

    private int[] encontrarMaiorSubArray(int[] array, int menor, int maior) {
        if (menor == maior) {
            return new int[]{menor, maior, array[menor]};
        }
        int meio = (menor + maior) / 2;

        int[] esquerda = encontrarMaiorSubArray(array, menor, meio); // T (n/2)
        int[] direita = encontrarMaiorSubArray(array, meio + 1, maior); // T (n/2)
        int[] cruzado = encontrarMaiorSubArrayCruzado(array, menor, meio, maior); // 0 (n)

        if (esquerda[2] >= direita[2] && esquerda[2] >= cruzado[2]) {
            setResultado(esquerda);
            return esquerda;
        } else if (direita[2] >= esquerda[2] && direita[2] >= cruzado[2]) {
            setResultado(direita);
            return direita;
        } else {
            setResultado(cruzado);
            return cruzado;
        }
    }

    private void setResultado(int[] resultado) {
        periodoMaisIntenso = resultado;
        inicio = resultado[0] + 1;
        fim = resultado[1] + 1;
        maiorAcumulo = resultado[2];
    }
    //#endregion

    @Override
    public String toString() {
        if(temperaturas.length > 366) {
            int anoInicio = (inicio / 365) + 1993;
            int diaInicio = inicio % 365;
            int anoFim = (fim / 365) + 1993;
            int diaFim = fim % 365;
            return "Período de Maior Acúmulo de Temperatura entre todos os anos {" +
                    "anoInicio: " + anoInicio + ", diaInicio: " + diaInicio + " || " +
                    "anoFim: " + anoFim + ", diaFim: " + diaFim +
                    ", maiorAcumulo: " + maiorAcumulo + "ºC }";
//                    + "\nTempo de Execução: " + tempoDeExecucao + " ns";
        }
        return "Ano " + ano + " - Período de Maior Acúmulo de Temperatura  { inicio: " + inicio + ", fim: "
                + fim + ", maiorAcumulo: " + maiorAcumulo + "ºC }";
//                + "\nTempo de Execução: " + tempoDeExecucao + " ns";
    }
}
