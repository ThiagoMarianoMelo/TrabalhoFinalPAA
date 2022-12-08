package org.example;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Stream;

public class App {
    public static void main(String[] args) {

//        Path path = Paths.get("temperaturas.txt");
        Path path = Paths.get("temperaturasExtremo.txt");

        List<AnoTemperatura> anoTemperaturaList = new ArrayList<AnoTemperatura>();
        List<Integer> temperaturasDeTodosAnos = new ArrayList<Integer>();

        try (Stream<String> stream = Files.lines(path)) {
            stream.forEach(line -> {
                String[] array = line.split(";");
                int[] arrayInt = new int[array.length+1];
                for (int i = 0; i < array.length; i++) {
                    temperaturasDeTodosAnos.add(Integer.parseInt(array[i]));
                    arrayInt[i] = Integer.parseInt(array[i]);
                }
                AnoTemperatura temperaturaAno = new AnoTemperatura(arrayInt);
                anoTemperaturaList.add(temperaturaAno);
            });
        } catch (IOException e) {
            e.printStackTrace();
        }

        System.out.println("\n**** CONSIDERANDO ACÚMULO (SOMA) DE DIFERENCAS DE TEMPERATURA ENTRE OS DIAS ****\n");
//        System.out.println("\n **** CONSIDERANDO ACÚMULO (SOMA) DE TEMPERATURAS PROPRIAMENTE DITAS ****");

        AnoTemperatura intervaloCompleto = new AnoTemperatura(temperaturasDeTodosAnos.stream().mapToInt(i -> i).toArray());
        System.out.println(intervaloCompleto + "\n");

        for (int i = 0; i < anoTemperaturaList.size(); i++) {
            anoTemperaturaList.get(i).setAno(1993 + i);
            System.out.println(anoTemperaturaList.get(i));
        }
        System.out.println("\nCOINCIDENCIAS:\n");

        List<Coincidencia> coincidencias = new ArrayList<Coincidencia>();
        for (int i = 0; i < anoTemperaturaList.size(); i++) {
            AnoTemperatura ano1 = anoTemperaturaList.get(i);

            for (int j = i + 1; j < anoTemperaturaList.size(); j++) {
                AnoTemperatura ano2 = anoTemperaturaList.get(j);

                if (verificaCoincidencia(ano1, ano2)) {
                    Coincidencia coincidencia = new Coincidencia(ano1, ano2);
                    coincidencias.add(coincidencia);
                    System.out.println(coincidencia);
                }
            }
        }



    }

    public static boolean verificaCoincidencia(AnoTemperatura ano1, AnoTemperatura ano2) {
        if(ano1.getInicio() >= ano2.getInicio() && ano1.getInicio() <= ano2.getFim()) {
            return true;
        } else if (ano1.getFim() <= ano2.getFim() && ano1.getFim() >= ano2.getInicio()) {
            return true;
        } else if(ano2.getInicio() >= ano1.getInicio() && ano2.getInicio() <= ano1.getFim()) {
            return true;
        } else if (ano2.getFim() <= ano1.getFim() && ano2.getFim() >= ano1.getInicio()) {
            return true;
        } else {
            return false;
        }
    }

}
