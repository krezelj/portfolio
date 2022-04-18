function [roots, period] = findroots(A, varargin)
% FINDROOTS Find real roots of a trigonometric polynomial
%   FINDROOTS(A) Finds real roots of a trigonometric polynomial given by the
% 	equation P(x) = Sum(A_i.*cos(i*x)) in the range [0, T] where T is the
% 	period of the polynomial.
%
%   INPUT:
%       A - vector of the polynomial coefficients
%
%   OUTPUT:
%       roots - calculated roots between [0, T] where T is the period of
%       the given polynomial
%       period - period of the given polynomial
%
%   PARAMETERS:
%       n           - number of initial guesses 
%                   default = 1001
%       maxIter     - maximum number of iterations performed to find a root
%                   default = 30
%       tolerance   - tolerance of root finding
%                   default = 1000*eps()
%       returnIn2Pi - decides whether the function should return all roots in
%                   range [0, 2*pi] 
%   EXAMPLES:
%       % find roots of cos(2x) + 3*cos(4x) - 1
%       findroots([0 0 1 0 3])

% Argument validation
p = inputParser;
defaultN = 1001;
defaultMaxIter = 30;
defaultTolerance = 1000*eps();
defaultReturnIn2Pi = false;

%isinteger() doesn't work for doubles
validScalarPosInt = @(x) isnumeric(x) && isscalar(x) && (x > 0) && floor(x) == x; 
validScalarPosNum = @(x) isnumeric(x) && isscalar(x) && (x > 0);
validScalarLogical = @(x) islogical(x) && isscalar(x);

addRequired(p, 'A', @isvector);
addParameter(p,'n',defaultN,validScalarPosInt);
addParameter(p,'maxIter',defaultMaxIter,validScalarPosInt);
addParameter(p,'tolerance',defaultTolerance,validScalarPosNum);
addParameter(p,'returnIn2Pi',defaultReturnIn2Pi,validScalarLogical);
parse(p, A, varargin{:});

N = p.Results.n;
maxIter = p.Results.maxIter;
tolerance = p.Results.tolerance;
returnIn2Pi = p.Results.returnIn2Pi;

% -------------------------------------------------------------------------
A = reshape(A(:), 1, numel(A)); % ensure A is a row vector
if (all(A==0))
    % constant function with every element of its domain being a root
    period = 2*pi;
    roots = 0;
    return;
end
if (length(A) == 1 && A(1) ~= 0)
    % constant function with no roots
    period = 2*pi;
    roots = [];
    return;
end

x = getinitpoints(A, N);
B = -A.*(0:(length(A)-1)); % Coefficients in the derivative

f = @(x) real(goertzel(A, x, true));
df = @(x) imag(goertzel(B, x, true));

period = calculateperiod(A);
roots = newtonroots(x, f, df, ...
    'maxIter', maxIter, ...
    'tolerance', tolerance, ...
    'rootRange', [0,period/2]);

roots = [roots, flip(period - roots)];

% remove possibly duplicated root
midpoint = length(roots)/2;
if (~isempty(roots))
    if (ismembertol(roots(midpoint), roots(midpoint+1), 10^5*tolerance))
       roots = roots(1:length(roots) ~= midpoint);  
    end
    if (ismembertol(roots(end), period, 10^5*tolerance))
        roots = roots(1:end-1);
    end
end

if (returnIn2Pi)
   offset =  repmat((0:2*pi/period - 1), length(roots), 1) * period;
   offset = offset(:)';
   roots = repmat(roots, 1, 2*pi/period);
   roots = roots + offset;
end
end

