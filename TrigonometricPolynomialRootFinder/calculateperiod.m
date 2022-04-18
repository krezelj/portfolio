function [period] = calculateperiod(A)
% CALCRANGE Calculate range for trigonometric polynomial root finding.
%   CALCRANGE(A) Calculates range for finding the roots of trigonometric
%   polynomials given by the equation P(x) = Sum(A_i.*cos(i*x)).
%   Function returns the higher end of range as the lower end is assumed to
%   be 0.
% 
%   INPUT:
%       A - vector of the polynomial coefficients
%
%   OUTPUT:
%       period - period of the polynomial
%   
%   PARAMETERS: None

if ~(isnumeric(A) && isvector(A))
    error("Calcrange:invalidCoefficientsVector."); 
end

A = reshape(A(:), 1, numel(A)); % ensure A is a row vector
if (length(A) == 1 || (length(A) > 1 && A(2) ~= 0) || all(A == 0))
    % if only A_0 is non zero then the polynomial is a constant function
    % if A_1 is non zero then the range will always be 2*pi
    period = 2*pi;
    return
end

% The period of cos(k*x) where i is a natural number is equal to 2pi/k
% If a coefficent A_k is zero it does not affect the overall period of the
% polynomial, so only A_k not equal zero are considered.
denominators = find(A(2:end)); % indices of non-zero A_k

% The lowest common multiple of fractions in the form of 1/k where k is a
% natural number is 1/g where G is the greatest common divisor of all
% denominators.
g = denominators(1);
if (isscalar(denominators))
   period = 2*pi / g;
   return;
end
for i = 2:length(denominators)
    g = gcd(denominators(i), g);
end
period = 2*pi/g;
end

