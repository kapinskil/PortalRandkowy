import { CanDeactivate } from '@angular/router';
import { UserEditComponent } from '../users/user-edit/user-edit.component';

export class PreventUnsavedChanges implements CanDeactivate<UserEditComponent> {
    canDeactivate(componet: UserEditComponent) {
        if (componet.editForm.dirty) {
            return confirm('Jesteś pewien, że chcesz kontunować? Wszystkie niezapisane dane zostaną utracone');
        }
        return true;
    }
}
