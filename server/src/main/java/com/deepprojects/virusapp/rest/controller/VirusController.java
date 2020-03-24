package com.deepprojects.virusapp.rest.controller;

import com.deepprojects.virusapp.rest.request.CreateVirusRequest;
import com.deepprojects.virusapp.rest.response.CreateVirusResponse;
import com.deepprojects.virusapp.service.VirusService;
import com.deepprojects.virusapp.util.Constants;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

@RestController("VirusController")
@RequestMapping(Constants.VERSION_ONE)
@RequiredArgsConstructor
public class VirusController {

    private final VirusService virusService;

    @RequestMapping(value = {"/virus"}, method = RequestMethod.POST)
    @ResponseStatus(HttpStatus.CREATED)
    public CreateVirusResponse createVirus(@Validated @RequestBody CreateVirusRequest createVirusRequest) {
        return CreateVirusResponse
                .builder()
                .virus(virusService.createVirus(createVirusRequest))
                .build();
    }

    @RequestMapping(value = {"/virus/image/{virusId}"}, method = RequestMethod.POST)
    public void uploadVirusImage(@PathVariable(name = "virusId") Long virusId,
                            @RequestParam("file") MultipartFile file) {
        virusService.uploadImage(file, virusId);
    }

    @RequestMapping(value = {"/public/virus/random"}, method = RequestMethod.GET)
    public CreateVirusResponse getRandomVirus() {
        return CreateVirusResponse.builder().virus(virusService.getOne()).build();
    }
}
