import { HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';

export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZXhwIjoxNzE1MDA0MDM5LCJpc3MiOiIxNWZXQCQlViZoZ1ckJmdmY3dlNUBGJjI0NTdGI0A0NSZGMzQmXiVGNDU2MzRnNTYzNDU2ZjM0XkZHIyQlXiozaCIsImF1ZCI6IjgzMjFuLTBmZDkyMS01NjBmMjNxNDZ2MjNeJEZAQCNeQ3Y0MjM0aTQ1NmZ0ZzIzOEMjJF5WQzQ5NzZ0OThmZHMifQ.H5r99nWgHVJxraijuFnypwrJ9bWDG_a7ojK0r0_q7Q8';
    const authReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });

    return next.handle(authReq);
  }
}
