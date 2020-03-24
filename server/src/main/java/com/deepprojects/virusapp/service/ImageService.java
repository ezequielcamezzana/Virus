package com.deepprojects.virusapp.service;

import com.amazonaws.regions.Regions;
import com.amazonaws.services.s3.AmazonS3;
import com.amazonaws.services.s3.AmazonS3ClientBuilder;
import com.amazonaws.services.s3.model.CannedAccessControlList;
import com.amazonaws.services.s3.model.ObjectMetadata;
import com.amazonaws.services.s3.model.PutObjectRequest;
import com.deepprojects.virusapp.exception.ServerException;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

@Service
@Slf4j
public class ImageService {

    private final AmazonS3 amazonS3;
    private final String bucketName;

    public ImageService(@Value("${virus.image.bucket}") String bucketName) {
        this.amazonS3 = AmazonS3ClientBuilder.standard().withRegion(Regions.US_EAST_2).build();
        this.bucketName = bucketName;
    }

    public String uploadImage(MultipartFile multipartFile, Long virusId, String seed) {
        try {
            String[] split = multipartFile.getOriginalFilename().split("\\.");
            String extension = split[split.length - 1];
            String key = String.format("%s_%s.%s", virusId, seed, extension);
            PutObjectRequest putObjectRequest = new PutObjectRequest(bucketName, key, multipartFile.getInputStream(), new ObjectMetadata())
                    .withCannedAcl(CannedAccessControlList.PublicRead);
            amazonS3.putObject(putObjectRequest);
            return key;
        } catch (Exception e) {
            log.error("Failed to load image", e);
            throw new ServerException();
        }
    }
}
