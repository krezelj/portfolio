function [roots] = filterroots(roots, f, varargin)
% FILTERROOTS Filter roots removing false roots and duplicate roots
%   FILTERROOTS(roots, f) Filters roots of function f removing false roots (i.e
%   roots that do not change sign in a default tolerance range) and duplicate
%   roots leaving only unique ones. One of the duplicates will always be
%   included in the result.
%
%   INPUT:
%       roots - vector of roots that will be filtered
%       f - handle to the function that the roots belong to
%
%   OUTPUT:
%       roots - vector of filtered roots
%
%   PARAMETERS:
%       tolerance - the tolerance of the filtering, usually should be the
%           same as the tolerance used to find the roots
%           default = 1000*eps()
%       filterRange - desired range of the roots. Any roots outside of that
%           range will be filtered out
%           default = [-inf, inf]
%       allowDuplicates - decides whether duplicate roots should be removed
%           (one of the duplicates will always remain)
%           default = false
%
%   EXAMPLES 
%       filterroots([-2, -1.01, -1.00, -0.99, 1.0], @(x) x.^2 - 1,...
%                       'filterRange', [-inf, 0],
%                       'tolerance', 0.05)
%       this call will return [-1.01]


% Argument validation
p = inputParser;

defaultTolerance = 1000*eps();
defaultRange = [-inf, inf];
defaultAllowDuplicates = false;

validNumVector = @(x) isnumeric(x) && isvector(x);
validFunctionHandle = @(x) isa(x, 'function_handle');
validScalarPosNum = @(x) isnumeric(x) && isscalar(x) && (x > 0);
validRange = @(x) isnumeric(x) && isvector(x) && length(x) == 2 && x(1) < x(2);
validScalarLogical = @(x) islogical(x) && isscalar(x);

addRequired(p, 'roots', validNumVector);
addRequired(p, 'f', validFunctionHandle);
addParameter(p, 'tolerance',defaultTolerance, validScalarPosNum);
addParameter(p, 'filterRange',defaultRange, validRange);
addParameter(p, 'allowDuplicates', defaultAllowDuplicates, validScalarLogical);

parse(p, roots, f, varargin{:});
tolerance = p.Results.tolerance;
range = p.Results.filterRange;
allowDuplicates = p.Results.allowDuplicates;

% -------------------------------------------------------------------------

% Range
roots = roots(roots <= range(2) & roots >= range(1));

% Duplicates
if (~allowDuplicates && ~isempty(roots))
    % datascale = 1 so that outliers don't mess with the tolerance
    roots = uniquetol(roots, tolerance);
end

% False roots
yLow = f(roots - tolerance);
yHigh = f(roots + tolerance);
roots = roots(yLow .* yHigh <= 0);
% while it is possible that between y_low and y_high the sign of the function
% would change twice the chances of that are very slim and the cost of
% additional computation is not worth it. 
% This is what the term 'tolerance' means afterall.
end

