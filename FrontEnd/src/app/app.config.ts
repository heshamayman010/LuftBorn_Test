import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withHashLocation } from '@angular/router';
import { routes } from './app.routes';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient, withInterceptorsFromDi, HTTP_INTERCEPTORS } from '@angular/common/http'; 
import { provideAnimations } from '@angular/platform-browser/animations';

import {
  IPublicClientApplication,
  PublicClientApplication,
  InteractionType,
  BrowserCacheLocation,
  LogLevel,
} from '@azure/msal-browser';

import {
  MsalInterceptor,
  MSAL_INSTANCE,
  MsalInterceptorConfiguration,
  MsalGuardConfiguration,
  MSAL_GUARD_CONFIG,
  MSAL_INTERCEPTOR_CONFIG,
  MsalService,
  MsalGuard,
  MsalBroadcastService,
} from '@azure/msal-angular';

import { environment } from '../environments/environment';

export function loggerCallback(logLevel: LogLevel, message: string) {
  console.log(message);
}

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: environment.config.adb2cConfig.clientId,
      authority: environment.config.adb2cConfig.authorityDomain, 
      knownAuthorities: ['HeshamEcommerce.ciamlogin.com'],
      redirectUri: 'https://localhost:4200', 
      postLogoutRedirectUri: 'https://localhost:4200',
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
    },
    system: {
      allowPlatformBroker: false, 
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false,
      },
    },
  });
}


// 2. Map backend routes so MSAL knows when to attach tokens
export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();
  
  environment.config.adb2cConfig.apiEndpointUrls.forEach(apiEndpointUrl => {
    protectedResourceMap.set(
      apiEndpointUrl, 
      environment.config.adb2cConfig.scopeUrls
    );
  });
  
  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap,
    strictMatching: false 
  };
}

//  Configure the Guard parameters to lock down routes if the user isnt logged in
export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: [...environment.config.adb2cConfig.scopeUrls],
    },
    loginFailedRoute: '/',
  };
}

export const appConfig: ApplicationConfig = {
  providers: [    
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withHashLocation()),
    provideAnimations(),
    
    //Keep only withInterceptorsFromDi() and DO NOT add withFetch() here
    provideHttpClient(withInterceptorsFromDi()),
    
    importProvidersFrom(BrowserModule),
    
    //  Explicitly link MSAL interceptor into Angular's modern DI scope pipeline
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true,
    },
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory,
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory,
    },
    
    // Core MSAL Angular services exposed for injection
    MsalService,
    MsalGuard,
    MsalBroadcastService,
  ],
};