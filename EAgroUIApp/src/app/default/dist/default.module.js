"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.DefaultModule = void 0;
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var home_component_1 = require("./home/home.component");
var contact_component_1 = require("./contact/contact.component");
var privacy_component_1 = require("./privacy/privacy.component");
var DefaultModule = /** @class */ (function () {
    function DefaultModule() {
    }
    DefaultModule = __decorate([
        core_1.NgModule({
            declarations: [
                home_component_1.HomeComponent,
                contact_component_1.ContactComponent,
                privacy_component_1.PrivacyComponent
            ],
            imports: [
                common_1.CommonModule
            ],
            exports: [
                home_component_1.HomeComponent
            ]
        })
    ], DefaultModule);
    return DefaultModule;
}());
exports.DefaultModule = DefaultModule;
