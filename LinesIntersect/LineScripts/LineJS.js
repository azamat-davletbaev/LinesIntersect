var dx = 50; //масштаб        
function DrawOneLine(x1, y1, x2, y2, clr) {    //функция рисует одну линию заданного цвета
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    ctx.beginPath();
    ctx.moveTo(x1 * dx, y1 * dx);
    ctx.lineTo(x2 * dx, y2 * dx);
    ctx.strokeStyle = clr;
    ctx.stroke();
};

var myApp = angular.module("myApp", []);
myApp.controller("myController", ['$scope', '$http',
    function ($scope, $http) {
        angular.element(document).ready(function () {
            $scope.GetLines();
            $scope.GetPolygon();
        });
        //---------------------
        $scope.LinesList = [];
        $scope.Polygon = {};
        $scope.GetLines = function () {
            return $http({
                method: 'POST',
                url: "Home/GetLines"
            }).then(function (response) {
                $scope.LinesList = response.data;
                $scope.LinesList.forEach(function (item, i, LinesList) {
                    DrawOneLine(item.P1.X, item.P1.Y, item.P2.X, item.P2.Y, item.Color);
                });
                return response.data;
            });
        };

        $scope.GetPolygon = function () {
            return $http({
                method: 'POST',
                url: "Home/GetPolygon"
            }).then(function (response) {
                $scope.Polygon = response.data;
                $scope.Polygon.Edges.forEach(function (item, i, Edges) {
                    DrawOneLine(item.P1.X, item.P1.Y, item.P2.X, item.P2.Y, item.Color);
                });
                return response.data;
            });
        };
    }]);