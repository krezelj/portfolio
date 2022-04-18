function [result, u, v] = goertzel(A, z, trig_convert)
% GOERTZEL Evaluate polynomial using goertzel algorithm
%   GOERTZEL(A, z) Evaluates a polynomial given by the equation 
%   w(x) = Sum(A_k*x.^k) at argument z.
%
%   GOERTZEL(A, z, trig_convert) Evaluates a polynomial given by the
%   equation w(x) = Sum(A_k*x.^k) at argument t = cos(z) + 1i*sin(z)
%   Used to evaluate trigonometric polynomials at argument z.
%
%   INPUT:
%       A       - vector of the polynomial coefficients
%       z       - arguments of the polynomial
%       trig_convert - decides whether all arguments should be converted to
%       t = cos(z) + 1i*sin(z)
%
%   OUTPUT:
%       result  - the result of the evaluation
%       u       - the 'real' part of the evaluation
%       v       - the 'imaginary' part of the evaluation
%
%   EXAMPLES: 
%       Calculate cos(x) + 5cos(3x) for z = linspace(0,2*pi,100)
%       [~, result, ~] = goertzel([0 1 0 5], linspace(0,2*pi,100), trigconvert)

narginchk(2, 4);
if ~(isvector(z) && isfloat(z))
    if (isempty(z))
        result = [];
        u = [];
        v = [];
        return; 
    end
    error("Goertzel:invalidZArgument");
elseif nargin == 3 && ~(isscalar(trig_convert) && islogical(trig_convert))
    error("Goertzel:invalidTrigConvert");
elseif ~(isvector(A))
    error("Goertzel:invalidCoefficientsVector");
end

A = reshape(A(:), 1, numel(A)); % ensure A is a row vector
z = reshape(z(:), 1, numel(z));

if (length(A)==1)
    result = repmat(A(1), 1, length(z));
    return;
end

if (nargin == 3 && trig_convert)
   x = cos(z);
   y = sin(z);
   p = 2*x;
   q = -1; % -(cos(x)^2 + sin(x)^2) ALWAYS -1
else
    x = real(z);
    y = imag(z);
    p = 2*x;
    q = -(x.^2 + y.^2);
end

N = length(A) - 1;
B = zeros(N+2, length(p));
B(end-1,:) = A(end);
for i = N-1:-1:1
    idx = i+1;
    B(idx,:) = A(idx) + p.*B(idx+1,:) + q.*B(idx+2,:);
end
u = A(1) + x.*B(2,:) + q.*B(3,:);
v = y.*B(2,:);
result = u + v.*1i;
end