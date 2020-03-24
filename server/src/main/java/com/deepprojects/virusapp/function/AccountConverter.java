package com.deepprojects.virusapp.function;

import com.deepprojects.virusapp.domain.AccountEntity;
import com.deepprojects.virusapp.rest.response.Account;
import org.springframework.stereotype.Service;

import java.util.stream.Collectors;

@Service
public class AccountConverter {

    private VirusConverter virusConverter;

    public AccountConverter(VirusConverter virusConverter) {
        this.virusConverter = virusConverter;
    }

    public Account toAccount(AccountEntity accountEntity) {
        return Account.builder().id(accountEntity.getId())
                .email(accountEntity.getEmail())
                .name(accountEntity.getName())
                .viruses(accountEntity.getViruses().stream().map(virusConverter::toVirus).collect(Collectors.toList()))
                .build();
    }
}
