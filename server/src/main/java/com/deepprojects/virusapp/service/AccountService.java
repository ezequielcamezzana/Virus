package com.deepprojects.virusapp.service;

import com.deepprojects.virusapp.domain.AccountEntity;
import com.deepprojects.virusapp.exception.EmailAlreadyExistsException;
import com.deepprojects.virusapp.exception.InvalidCredentialsException;
import com.deepprojects.virusapp.function.AccountConverter;
import com.deepprojects.virusapp.function.AuthenticatedUser;
import com.deepprojects.virusapp.repository.AccountRepository;
import com.deepprojects.virusapp.rest.request.CreateAccountRequest;
import com.deepprojects.virusapp.rest.request.CreateSessionRequest;
import com.deepprojects.virusapp.rest.request.ModifyAccountRequest;
import com.deepprojects.virusapp.rest.request.RefreshSessionRequest;
import com.deepprojects.virusapp.rest.response.Account;
import com.deepprojects.virusapp.rest.response.CreateSessionResponse;
import com.deepprojects.virusapp.rest.response.GenericIdResponse;
import io.micrometer.core.instrument.util.StringUtils;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import javax.transaction.Transactional;
import java.util.Optional;

@Service
@Slf4j
@RequiredArgsConstructor
public class AccountService {

    private final AccountRepository accountRepository;
    private final PasswordEncoder passwordEncoder;
    private final AuthService authService;
    private final AccountConverter accountConverter;

    @Transactional
    public GenericIdResponse createAccount(CreateAccountRequest createAccountRequest) {
        checkIfEmailExists(createAccountRequest.getEmail());

        AccountEntity accountEntity = saveAccount(createAccountRequest.getEmail(),
                createAccountRequest.getName(), createAccountRequest.getPassword());

        return GenericIdResponse.builder()
                .id(accountEntity.getId())
                .build();

    }

    public CreateSessionResponse createSession(CreateSessionRequest createSessionRequest) {
        Optional<AccountEntity> byEmail = accountRepository.findFirstByEmail(createSessionRequest.getEmail());

        if (!byEmail.isPresent()) {
            throw new InvalidCredentialsException();
        }
        AccountEntity accountEntity = byEmail.get();

        if (!passwordEncoder.matches(createSessionRequest.getPassword(), accountEntity.getPassword())) {
            throw new InvalidCredentialsException();
        }

        return CreateSessionResponse
                .builder()
                .authToken(createToken(accountEntity))
                .userId(accountEntity.getId())
                .refreshToken(authService.createRefreshToken(accountEntity.getId(), accountEntity.getEmail(), accountEntity.getPassword()))
                .build();
    }

    public CreateSessionResponse refreshSession(RefreshSessionRequest refreshSessionRequest) {
        Optional<AccountEntity> byEmail = accountRepository.findFirstByEmail(refreshSessionRequest.getEmail());

        if (!byEmail.isPresent()) {
            throw new InvalidCredentialsException();
        }
        AccountEntity accountEntity = byEmail.get();

        if (!refreshSessionRequest.getRefreshToken().equals(authService.createRefreshToken(accountEntity.getId(),
                accountEntity.getEmail(), accountEntity.getPassword()))) {
            throw new InvalidCredentialsException();
        }

        return CreateSessionResponse
                .builder()
                .authToken(createToken(accountEntity))
                .userId(accountEntity.getId())
                .build();
    }

    public void modifyAccount(ModifyAccountRequest modifyAccountRequest) {
        AuthenticatedUser currentUser = authService.getCurrentUser();

        AccountEntity account = accountRepository.getOne(currentUser.getUserId());

        if (!passwordEncoder.matches(modifyAccountRequest.getPassword(), account.getPassword())) {
            throw new InvalidCredentialsException();
        }

        if (StringUtils.isNotBlank(modifyAccountRequest.getNewPassword())) {
            account.setPassword(passwordEncoder.encode(modifyAccountRequest.getNewPassword()));
        }

        if (StringUtils.isNotBlank(modifyAccountRequest.getName())) {
            account.setName(modifyAccountRequest.getName());
        }

        accountRepository.save(account);
    }

    public Account getMyAccount() {
        AuthenticatedUser currentUser = authService.getCurrentUser();
        AccountEntity one = accountRepository.getOne(currentUser.getUserId());
        return accountConverter.toAccount(one);
    }

    private void checkIfEmailExists(String email) {
        Optional<AccountEntity> byEmail = accountRepository.findFirstByEmail(email);

        if (byEmail.isPresent()) {
            throw new EmailAlreadyExistsException();
        }
    }

    private String createToken(AccountEntity accountEntity) {
        return authService.getToken(accountEntity.getId());
    }

    private AccountEntity saveAccount(String email, String name, String password) {
        AccountEntity account = AccountEntity.builder()
                .email(email)
                .name(name)
                .password(passwordEncoder.encode(password))
                .build();

        return accountRepository.save(account);
    }
}
