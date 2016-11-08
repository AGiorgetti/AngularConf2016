import { NgModule, forwardRef } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { UpgradeAdapter } from '@angular/upgrade';

import { TestComponent } from "./test/test.component";
import { HeartbeatView } from "./Dashboard/heartbeatView.component";
import { LogsService } from "./Services/logsService";
import { MomentPipe } from "./Shared/moment.pipe";

// we still need to have a <script> reference to angularjs 1.x to have the old application (not loaded with a module loader
// working), to have this working in visual studio we can:
// - include the @types/angular .d.ts files in the solution (which is a thing we could have already done if we used TypeScript 
//   to create the angular 1.x project), but the module loader will then try to load the
//   angular framework again, and we have already loaded it with the <script> tags at the application startup
//   
// - avoid using them and just declare an brand new 'angular' module (as done here)
declare var angular: ng.IAngularStatic; // https://github.com/Microsoft/TypeScript/issues/10638

// the only adapter instance 
export const adapter = new UpgradeAdapter(forwardRef(() => AppModule));

angular.module('app')
    .directive('ng2HeartbeatView', adapter.downgradeNg2Component(HeartbeatView) as ng.IDirectiveFactory);
angular.module('app')
    .service('ng2LogService', adapter.downgradeNg2Provider(LogsService));
angular.module('app')
    .directive('ng2Test', adapter.downgradeNg2Component(TestComponent) as ng.IDirectiveFactory);

adapter.upgradeNg1Provider('logsService');

@NgModule({
    imports: [BrowserModule, HttpModule, FormsModule],
    declarations: [
        adapter.upgradeNg1Component("sidMonitoringHeartbeats"),
        TestComponent,
        HeartbeatView,
        MomentPipe
    ],
    providers: [LogsService]
})
export class AppModule {
}

// hybrid bootstrap
adapter.bootstrap(document.body, ["app"], { strictDi: true });