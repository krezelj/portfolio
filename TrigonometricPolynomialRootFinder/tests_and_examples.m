%% Test 1 -- cos(x)
% Podstawowe sprawdzenie działania funkcji
A = [0 1]; %cos(x)
roots = findroots(A);
disp(roots);
% Poprawnie wyznaczone miejsca zerowe pi/2 i 3*pi/2

%% Test 2 -- Funkcja stała równa 0
A = 0;
roots = findroots(A);
disp(roots); % cała dziedzina to miejsca zerowe
%% Test 3 -- Funkcja stała nierówna 0
A = rand(1); % zero prawie na pewno nie będzie wylosowane
roots = findroots(A);
disp(roots);
disp(length(roots)); % pusty wektor nie jest pokazywany

%% Test 4 -- cos(10x)
A = zeros(1, 11);
A(11) = 1;
roots = findroots(A);
disp(roots);
% disp(real(goertzel(A, roots, true)));
realRoots = [0 0];
realRoots(1) = fzero(@(x) cos(10*x), [0, pi/10]);
realRoots(2) = fzero(@(x) cos(10*x), [pi/10, pi/5]);
disp(abs(realRoots-roots)./realRoots);
% funkcje wbudowane znajdują te same miejsca zerowe

%% Test 5 -- cos(100x) - 2*cos(50x) + 0.5
A = zeros(1, 101);
A(101) = 1;
A(51) = -2;
A(1) = 0.5;
roots = findroots(A);
format long
disp(roots);
% wynik wolfra
realRoots = [0 0];
realRoots(1) = 1/25*(atan(sqrt(5/7 + (4*sqrt(2))/7)));
realRoots(2) = 1/25*(pi-atan(sqrt(5/7 + (4*sqrt(2))/7)));
disp(abs(realRoots-roots)./realRoots);
% link do porównania na wolfram
% https://www.wolframalpha.com/input/?i=%7Bcos%28100x%29+-+2*cos%2850x%29+%2B+0.5+%3D+0%2C+0+%3C+x+%3C+0.1%7D
% wynik jest ten sam
%% Test 6
M = 120;
startM = 50;
Amax = 3;
Amin = -3;
mse = zeros(1,M-startM+1);
for i = startM:M
    A = rand(1, i) * (Amax - Amin) + Amin;
    period = calculateperiod(A);
    roots = findroots(A);

    realRoots = [];
    initPoints = getinitpoints(A, 1001);
    initPoints = [initPoints, flip(period - initPoints)];
    f = @(x) real(goertzel(A, x, true));
    for j = 1:length(initPoints)
        realRoots(j) = fzero(f, initPoints(j));
    end
    % realRoots = uniquetol(realRoots, 1000*eps(),'DataScale',1);
    if (length(roots)<length(realRoots))
       disp("Nie znaleziono takiej samej liczby zer dla i =");
       disp(i);
       break;
    end
    roots = sort(roots);
    realRoots = sort(realRoots);
    mse(i-startM+1) = sum((roots - realRoots).^2)/length(roots);
    if (mse(i-startM+1)>0.05)
       disp("err");
       disp("sprawdzić czy fzero nie znalazło tego samego zera kilka razy");
       disp("fzero czasami nie znajduje tego samego zera dla danej punktu początkowego");
    end
end
plot(startM:M, mse);
%%
%% Example 1 cos(10000x)
A = zeros(1, 10000);
A(end) = 1;
roots = findroots(A, 'n', 5001);
disp(roots);
% funkcja działa poprawnie nawet dla wielomianów stopnia 10000
%% Example 2 k=0,1,...,5000 A_k != 0
A = rand(1, 5000) * 2 - 1;
tic
roots = findroots(A);
toc
disp(length(roots));
% funkcja działa szybko dla bardzo długich wielomianów (chociaż być może
% nie znajduje wszystkich zer);
tic
roots = findroots(A, 'n', 10001);
toc
disp(length(roots));
tic
roots = findroots(A, 'n', 100001);
toc
disp(length(roots));
% zwiększenie liczby podziałów zwiększa czas oczekiwania, ale znajduje
% także więcej zer
disp(sum(abs(real(goertzel(A, roots, true))) > 10^(-8)));
% funkcja faktycznie znajduje miejsca zerowe (wymogiem w filterroots jest
% zmiana znaku w okolicy miejsca zerowego)
%% Example 3 cos(2x) + cos(5x)
A = [0 0 1 0 0 1];
roots = findroots(A);
format short
display(roots);
% funkcja znajduje miejsca zerowe w których pochodna jest równa 0
%% Example 4 cos(1000x) - cos(999x)
A = zeros(1, 1001);
A(end) = 1;
A(end-1) = -1;
roots = findroots(A, 'n', 3001);
disp(length(roots));
% funkcja sprawnie znajduje miejsca zerowe nawet jeśli okres wielomianu to
% 2pi
%% Example 5 -cos(1000x) + cos(3500x) + cos(5000x) - cos(8000x)
A = zeros(1, 8001);
A(1001) = -1;
A(3501) = 1;
A(5001) = 1;
A(8001) = -1;
roots = findroots(A);
disp(sort(roots)');
% tyle samo miejsc zerowych jak przy użyciu wolfram
% w Wolfram należy użyć "more solutions" tak długo aż pokażą się wszystkie
%https://www.wolframalpha.com/input/?i=%7B-cos%281000x%29%2Bcos%283500x%29%2Bcos%285000x%29-cos%288000x%29%3D+0%2C+0+%3C+x+%3C+0.0126%7D
%% Examples 6 cos(10x)-cos(100x)+cos(1000x)
A = zeros(1, 1001);
A(11) = 1;
A(101) = -1;
A(1001) = 1;
roots = findroots(A);
period = calculateperiod(A);
trigplot(A, 'specialPoints', roots, 'xmin', 0, 'xmax', period, 'n', 2001);