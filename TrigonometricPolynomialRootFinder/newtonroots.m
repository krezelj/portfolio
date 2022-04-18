function [roots, xPrev, iterations] = newtonroots(x, f, df, varargin)
% NEWTONROOTS Find roots of a function using Newton's method
%   NEWTONROOTS(x, f, df) Finds roots of a function f for starting points
%   given in the vector x. Argument f shall be the handle to the function
%   and df the handle to its derivatie.
%
%   INPUT:
%       x   - vector of initial guesses
%       f   - handle to the evaluated function
%       df  - handle to the derivative of the evaluated function
%
%   OUTPUT:
%       roots - vector of found roots
%       xPrev - vector of points after *their* last iteration. If
%       'returnAll' parameter is set x_prev stores value of every initial
%       guess (as columns) for every iteration (as rows)
%       iterations - iterations performed for each initial guess
%
%   PARAMETERS:
%       maxIter     - maximum number of iterations performed to find a root
%                       default = 30
%       tolerance   - tolerance of root finding
%                       default = 1000 * eps()
%       returnAll   - decides whether x_prev should return only one previous
%                       iteration or all previous iterations
%                       default = false
%
%   EXAMPLES: 
%       % find zeros for x^2 - 1
%       newtonroots([-2, 2], @(x) x.^2-1, @(x) 2*x)

% Argument validation
p = inputParser;
defaultMaxIter = 30;
defaultTolerance = 1000*eps();
defaultReturnAllIter = false;
defaultRange = [-inf, inf];

validScalarPosInt = @(x) isnumeric(x) && isscalar(x) && (x > 0) && floor(x) == x; 
validScalarPosNum = @(x) isnumeric(x) && isscalar(x) && (x > 0);
validVectorNum = @(x) isnumeric(x) && isvector(x);
validFunctionHandle = @(x) isa(x, 'function_handle');
validScalarLogical = @(x) islogical(x) && isscalar(x);
validRange = @(x) isnumeric(x) && isvector(x) && length(x) == 2 && x(1) < x(2);

addRequired(p,'x', validVectorNum);
addRequired(p,'f', validFunctionHandle);
addRequired(p,'df', validFunctionHandle);
addParameter(p,'maxIter',defaultMaxIter,validScalarPosInt);
addParameter(p,'tolerance',defaultTolerance,validScalarPosNum);
addParameter(p,'returnAllIterations',defaultReturnAllIter,validScalarLogical);
addParameter(p,'rootRange',defaultRange,validRange);
parse(p, x, f, df, varargin{:});

maxIter = p.Results.maxIter;
tolerance = p.Results.tolerance;
returnAll = p.Results.returnAllIterations;
rootRange = p.Results.rootRange;

% -------------------------------------------------------------------------
xNew = x;
xPrev = zeros(1, length(x));
included = true(1, length(x));
iterations = zeros(1, length(x));
if (returnAll)
    xAll = zeros(maxIter+1, length(x));
end

for i = 1:maxIter

    % Reassign
    xPrev(included) = xNew(included);
    if (returnAll)
       xAll(i,:) = xPrev;
       xAll(i,isinf(xPrev)) = nan;
    end
    
    % Check if any roots were found in the previous iteration (or if any of the
    % initial points was a root). Save values for later use
    y = f(xPrev(included));
    included(included) = included(included) & (y ~= 0);
    y = y(y~=0);
    
    % Perform iteration
    xNew(included) = xPrev(included) - y./df(xPrev(included));
    
    % Update iterations count
    iterations(included) = iterations(included) + 1;
    
    % Termination condition
    diff = xNew - xPrev;
    included = included & (abs(diff) >= tolerance) & ...
                ~isnan(xNew) & ~isinf(xNew);
    
    if (~any(included) && ~returnAll)
       break; % Exit condition if there is nothing to calculate anymore 
    end
end
% Set all still included as NaN (as they did not converge)
xNew(included) = nan;
xNew(isinf(xNew)) = nan;
if (returnAll)
   xAll(maxIter+1,:) = xNew; 
end

roots = filterroots(xNew, f, 'tolerance', tolerance,'filterRange',rootRange);
if (returnAll)
   xPrev = xAll; 
else
   xPrev = xNew; 
end
end

