"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var rxjs_1 = require("rxjs");
var RaceService = /** @class */ (function () {
    function RaceService(httpClient) {
        this.httpClient = httpClient;
        this.raceSubject = new rxjs_1.Subject();
        this.allrace = [];
    }
    RaceService.prototype.emitRaceSubject = function () {
        this.raceSubject.next(this.allrace.slice());
    };
    RaceService.prototype.getAllRaces = function () {
        var _this = this;
        this.httpClient.get('api/race/').subscribe(function (result) {
            console.log(result);
            _this.allrace = result;
            _this.emitRaceSubject();
        }, function (error) { return console.error(error); });
    };
    return RaceService;
}());
exports.RaceService = RaceService;
//# sourceMappingURL=race.service.js.map