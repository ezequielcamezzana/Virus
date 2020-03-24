package com.deepprojects.virusapp.rest.response;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@AllArgsConstructor
@NoArgsConstructor
@Data
@Builder
public class CreateAccountResponse {
    private Long userId;
    private String authToken;
    private String refreshToken;
}
