# Laboratoria - Platformy Programistyczne .NET i Java

**Autor:** Paweł Siewiera

**Technologia:** .NET 10.0 (C#)

**IDE:** Visual Studio 2022


## Laboratorium 1: Problem Plecakowy

Projekt polegał na stworzeniu aplikacji rozwiązującej problem plecakowy przy pomocy algorytmu zachłannego. Rozwiązanie składa się z trzech części: logiki, testów oraz interfejsu graficznego.

### Opis projektów:
* **Lab1:** Główny projekt zawierający logikę problemu.
    * `Item`: Klasa przechowująca wagę, wartość i indeks przedmiotu.
    * `Problem`: Odpowiada za generowanie przedmiotów (na podstawie ziarna) oraz wykonanie algorytmu Solve.
    * `Knapsack`: Klasa wynikowa z listą wybranych przedmiotów oraz ich łączną wagą i wartością.
* **Lab1GUI:** Aplikacja Windows Forms. Pozwala wpisać liczbę przedmiotów, ziarno losowania oraz pojemność plecaka. Wynik wyświetlany jest w polu tekstowym.
* **Lab1Tests:** Testy jednostkowe MSTest. Sprawdzają poprawność generowania danych, limity wagowe oraz sytuacje, gdy żaden przedmiot nie pasuje do plecaka.

### Wykorzystane mechanizmy:
* Algorytm zachłanny sortujący przedmioty według stosunku wartości do wagi.
* Obsługa błędów danych wejściowych w GUI za pomocą bloku try-catch.



## Laboratorium 2


## Laboratorium 3
