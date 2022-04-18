function [initPoints] = getinitpoints(A, N)
% GETINITPOINTS Calculate optimal initial points for trigonometric polynomial 
%   root finding

%   GETINITPOINTS(A, N) Calculates optimal initial points for finding roots
%   of a trigonometric polynomial given by the equation P(x) = Sum(A_i.*cos(i*x))
%   where N is the number of samples from range [0, T/2] where T is the period
%   of the polynomial.
%
%   INPUT:
%       A - vector of the polynomial coefficients
%       N - number of samples
%
%   OUTPUT:
%       initPoints - optimised initial guesses for the given polynomial
%
%   EXAMPLES:
%       % find best initial guesses for cos(10x) 
%       getinitpoints([0 0 0 0 0 0 0 0 0 0 1], 100);

period = calculateperiod(A);
xInit = linspace(0, period/2, N);
yInit = real(goertzel(A, xInit, true));
signChangeIdx = diff(sign(yInit)) ~= 0;
initPoints = xInit(signChangeIdx);
end

