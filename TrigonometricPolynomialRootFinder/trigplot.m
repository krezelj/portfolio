function [] = trigplot(A, varargin)
% TRIGPLOT Plot trigonometric polynomials
% 	TRIGPLOT(A) Plots a trigonometric polynomial given by the
% 	equation P(x) = Sum(A_i.*cos(i*x)) in range between xmin and xmax. 
%   The polynomial is evaluated at n equally spaced arguments.
%
%   INPUT:
%       A - vector of the polynomial coefficients
%   
%   OUTPUT: None
%
%   PARAMETERS:
%       n   - number of points at which the polynomial will be evaluated
%               defualt = 501
%       xmin - lower end of the range
%               default = -2*pi
%       xmax - upper end of the range
%               default = 2*pi
%       specialPoints - arguments that will be plotted as red crosses so
%           that they stand out from the rest of the plot.
% 
%   EXAMPLES:
%       % plot 0.5+1*cos(x)+3*cos(5*x) at 50 points between -1 and 1.
%       trigplot([0.5, 1, 0, 0, 0, 5], 'xmin', -1, 'xmax', 1, 'n', 50)
%
%       % plot cos(x) + 2*cos(2x) + 3*cos(3x) at 501 points between -2pi
%       and 2pi.
%       trigplot(0:3)
%       
%       % plot cos(x) with zeros as special points
%       trigplot([0 1], 'specialPoints', [pi/2, 3*pi/2])

% Argument validation
p = inputParser;
defaultN = 501;
defaultXMin = -2*pi;
defaultXMax = 2*pi;
defaultSp = [];

validScalarPosInt = @(x) isnumeric(x) && isscalar(x) && (x > 0) && floor(x) == x;
validScalarNum = @(x) isnumeric(x) && isscalar(x);
validVectorNum = @(x) isnumeric(x) && isvector(x);

addRequired(p, 'A', validVectorNum);
addOptional(p, 'xmin', defaultXMin, validScalarNum);
addOptional(p, 'xmax', defaultXMax, validScalarNum);
addParameter(p, 'n', defaultN, validScalarPosInt);
addParameter(p, 'specialPoints', defaultSp, validVectorNum);

parse(p, A, varargin{:});
xmin = p.Results.xmin;
xmax = p.Results.xmax;
N = p.Results.n;
xSpecial = p.Results.specialPoints;

% -------------------------------------------------------------------------
A = reshape(A(:), 1, numel(A)); % ensure A is a row vector
x = linspace(xmin, xmax, N);
y = real(goertzel(A, x, true));

if (~isempty(xSpecial))
    y_special = real(goertzel(A, xSpecial, true));
else
    y_special = [];
end

hold on
plot(x, y, 'b-', xSpecial, y_special, '.r');
hold off

grid on
title("Trigonometric polynomial with coefficents A");
xlim([xmin, xmax]);
xlabel("x");
ylabel("P(x)");

end

