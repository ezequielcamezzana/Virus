package com.deepprojects.virusapp.rest.controller;

import com.deepprojects.virusapp.rest.request.*;
import com.deepprojects.virusapp.rest.response.Account;
import com.deepprojects.virusapp.rest.response.CreateSessionResponse;
import com.deepprojects.virusapp.rest.response.GenericIdResponse;
import com.deepprojects.virusapp.service.AccountService;
import com.deepprojects.virusapp.util.Constants;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

@RestController("AccountController")
@RequestMapping(Constants.VERSION_ONE)
@RequiredArgsConstructor
public class AccountController {

    private final AccountService accountService;

    @RequestMapping(value = {"/account"}, method = RequestMethod.POST)
    @ResponseStatus(HttpStatus.CREATED)
    public GenericIdResponse createAccount(@Validated @RequestBody CreateAccountRequest createAccountRequest) {
        return accountService.createAccount(createAccountRequest);
    }

    @RequestMapping(value = {"/account/session"}, method = RequestMethod.POST)
    public CreateSessionResponse createSession(@Validated @RequestBody CreateSessionRequest createSessionRequest) {
        return accountService.createSession(createSessionRequest);
    }

    @RequestMapping(value = {"/account/session/refresh"}, method = RequestMethod.POST)
    public CreateSessionResponse refreshSession(@Validated @RequestBody RefreshSessionRequest refreshSessionRequest) {
        return accountService.refreshSession(refreshSessionRequest);
    }

    @RequestMapping(value = {"/me/account"}, method = RequestMethod.PUT)
    public void changePassword(@Validated @RequestBody ModifyAccountRequest modifyAccountRequest) {
        accountService.modifyAccount(modifyAccountRequest);
    }

    @RequestMapping(value = {"/me/account"}, method = RequestMethod.GET)
    public Account getMyAccount() {
        return accountService.getMyAccount();
    }

}
