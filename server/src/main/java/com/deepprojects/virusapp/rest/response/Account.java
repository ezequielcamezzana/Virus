package com.deepprojects.virusapp.rest.response;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@AllArgsConstructor
@NoArgsConstructor
@Data
@Builder
public class Account {

    private String email;
    private String name;
    private Long id;
    private String avatar;
    private List<Virus> viruses;

}
