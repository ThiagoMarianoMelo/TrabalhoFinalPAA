package org.example;

public class Coincidencia {

    AnoTemperatura ano1;
    AnoTemperatura ano2;
    int inicioCoincidencia;
    int fimCoincidencia;

    public Coincidencia(AnoTemperatura ano1, AnoTemperatura ano2) {
        this.ano1 = ano1;
        this.ano2 = ano2;
        setInicioEFim();
    }

    private void setInicioEFim() {
        if(ano1.getInicio() < ano2.getInicio()) {
            inicioCoincidencia = ano2.getInicio();
        } else {
            inicioCoincidencia = ano1.getInicio();
        }

        if(ano1.getFim() < ano2.getFim()) {
            fimCoincidencia = ano1.getFim();
        } else {
            fimCoincidencia = ano2.getFim();
        }
    }

    @Override
    public String toString() {
        return "Coincidencia { " +
                "ano1: " + ano1.getAno() +
                ", ano2: " + ano2.getAno() +
                ", inicioCoincidencia: " + inicioCoincidencia +
                ", fimCoincidencia: " + fimCoincidencia +
                " }";
    }
}
