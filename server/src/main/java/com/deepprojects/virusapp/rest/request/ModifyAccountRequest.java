package com.deepprojects.virusapp.rest.request;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.validation.constraints.NotBlank;

@AllArgsConstructor
@NoArgsConstructor
@Data
@Builder
public class ModifyAccountRequest {

    private String name;
    @NotBlank
    private String password;
    private String newPassword;
}
