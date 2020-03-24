package com.deepprojects.virusapp.repository;

import com.deepprojects.virusapp.domain.VirusEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface VirusRepository extends JpaRepository<VirusEntity, Long> {
}
