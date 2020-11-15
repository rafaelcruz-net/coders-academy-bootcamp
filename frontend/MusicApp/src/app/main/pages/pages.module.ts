import { NgModule } from "@angular/core";

import { LoginModule } from "app/main/pages/authentication/login/login.module";
import { RegisterModule } from "app/main/pages/authentication/register/register.module";
import { MusicModule } from "./music/music.module";

@NgModule({
    imports: [
        // Authentication
        LoginModule,
        RegisterModule,
        MusicModule,
    ],
})
export class PagesModule {}
