import { Component, OnDestroy, OnInit, ViewEncapsulation } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormGroup,
    ValidationErrors,
    ValidatorFn,
    Validators,
} from "@angular/forms";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/internal/operators";

import { FuseConfigService } from "@fuse/services/config.service";
import { fuseAnimations } from "@fuse/animations";
import { PersistenceService } from "app/services/persistence.service";
import { UserService } from "app/services/user.service";
import swal from "sweetalert2";
import { Router } from "@angular/router";

@Component({
    selector: "register",
    templateUrl: "./register.component.html",
    styleUrls: ["./register.component.scss"],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations,
})
export class RegisterComponent implements OnInit, OnDestroy {
    registerForm: FormGroup;

    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _service: UserService,
        private _persistence: PersistenceService,
        private _router: Router
    ) {
        this._fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: true,
                },
                toolbar: {
                    hidden: true,
                },
                footer: {
                    hidden: true,
                },
                sidepanel: {
                    hidden: true,
                },
            },
        };

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        this.registerForm = this._formBuilder.group({
            name: ["", Validators.required],
            email: ["", [Validators.required, Validators.email]],
            password: ["", Validators.required],
            passwordConfirm: [
                "",
                [Validators.required, confirmPasswordValidator],
            ],
        });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    register() {
        if (this.registerForm.invalid) {
            return;
        }

        let name = this.registerForm.get("name").value;
        let email = this.registerForm.get("email").value;
        let password = this.registerForm.get("password").value;

        this._service
            .register({
                email: email,
                password: password,
                name: name,
            })
            .subscribe(
                (data) => {
                    this._fuseConfigService.config = {
                        layout: {
                            navbar: {
                                hidden: false,
                            },
                            toolbar: {
                                hidden: false,
                            },
                        },
                    };
                    this._persistence.set("authenticate_user", data);
                    this._router.navigate(["/"]);
                },
                (error) => {
                    swal.fire(
                        "Ops!",
                        "Aconteceu um error ao criar sua conta",
                        "error"
                    );
                }
            );
    }
}

export const confirmPasswordValidator: ValidatorFn = (
    control: AbstractControl
): ValidationErrors | null => {
    if (!control.parent || !control) {
        return null;
    }

    const password = control.parent.get("password");
    const passwordConfirm = control.parent.get("passwordConfirm");

    if (!password || !passwordConfirm) {
        return null;
    }

    if (passwordConfirm.value === "") {
        return null;
    }

    if (password.value === passwordConfirm.value) {
        return null;
    }

    return { passwordsNotMatching: true };
};
